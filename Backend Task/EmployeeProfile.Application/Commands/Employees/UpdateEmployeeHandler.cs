using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;
using EmployeeProfile.Application.UnitOfWork;
namespace EmployeeProfile.Application.Commands.Employees;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
{
        private readonly IQueryRepository<Employee> _employeeReadRepository;
        private readonly ICommandRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
    public UpdateEmployeeHandler(IQueryRepository<Employee> employeeReadRepository 
        ,ICommandRepository<Employee> employeeRepository
        ,IUnitOfWork unitOfWork)
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public  Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee =  _employeeReadRepository.GetById(request.EmployeeNo);
        if (employee == null)
            throw new Exception($"Employee with number {request.EmployeeNo} not found");

        employee.Update(
            request.Name,
            request.HireDate,
            request.DepartmentId,
            request.OccupationId,
            request.GradeId
            );

        _employeeRepository.Update(employee);
        
        return Task.CompletedTask;
    }
}

