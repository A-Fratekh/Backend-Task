using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Application.UnitOfWork;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly ICommandRepository<Employee> _employeeCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeHandler(ICommandRepository<Employee> employeeCommandRepository, IUnitOfWork unitOfWork)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee ( request.Name, request.HireDate, request.DepartmentId, request.OccupationId, request.GradeId);
                await _employeeCommandRepository.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            return employee.EmployeeNo;
    }
}
