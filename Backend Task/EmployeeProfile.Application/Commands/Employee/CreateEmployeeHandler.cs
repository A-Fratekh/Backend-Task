using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly ICommandRepository<Employee> _employeeCommandRepository;

    public CreateEmployeeHandler(ICommandRepository<Employee> employeeCommandRepository)
    {
        _employeeCommandRepository = employeeCommandRepository;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee (request.EmployeeNo, request.Name, request.HireDate, request.DepartmentId, request.GradeId, request.OccupationId);
        await _employeeCommandRepository.AddAsync(employee);
        return employee.Id;
    }
}
