using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;
namespace EmployeeProfile.Application.Commands.Employees;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand>
{
        private readonly IQueryRepository<Employee> _employeeReadRepository;
        private readonly ICommandRepository<Employee> _employeeRepository;
    public UpdateEmployeeHandler(IQueryRepository<Employee> employeeReadRepository 
        ,ICommandRepository<Employee> employeeRepository
       )
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
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

