using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly ICommandRepository<Employee> _employeeCommandRepository;

    public CreateEmployeeHandler(ICommandRepository<Employee> employeeCommandRepository)
    {
        _employeeCommandRepository = employeeCommandRepository;
    }

    public Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee ( request.Name, request.HireDate, request.DepartmentId, request.OccupationId, request.GradeId);
         _employeeCommandRepository.Add(employee);

        return Task.FromResult(employee.EmployeeNo);
    }
}
