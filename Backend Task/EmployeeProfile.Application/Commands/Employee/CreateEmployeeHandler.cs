using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly ICommandRepository<Employee> _employeeCommandRepository;
    private readonly IQueryRepository<Employee> _employeeQueryRepository;


    public CreateEmployeeHandler(ICommandRepository<Employee> employeeCommandRepository, IQueryRepository<Employee> employeeQueryRepository)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee (request.EmployeeNo, request.Name, request.HireDate, request.DepartmentId, request.OccupationId, request.GradeId);
        IEnumerable<Employee> employees = await _employeeQueryRepository.GetAllAsync(null);
        try
        {

                foreach (var e in employees)
                {
                    if (e.EmployeeNo == employee.EmployeeNo)
                    {
                        throw new InvalidOperationException();
                    }
                }
                await _employeeCommandRepository.AddAsync(employee);
            return employee.Id;
        }
        catch
        {
            throw new InvalidOperationException($"Unable To Create Employee, Employee with Employee number : {employee.EmployeeNo} Already Exists");
        }
    }
}
