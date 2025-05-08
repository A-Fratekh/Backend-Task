using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;
namespace EmployeeProfile.Application.Commands.Employees;

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Guid>
{
        private readonly IQueryRepository<Employee> _employeeReadRepository;
        private readonly ICommandRepository<Employee> _employeeRepository;

    public UpdateEmployeeHandler(IQueryRepository<Employee> employeeReadRepository ,ICommandRepository<Employee> employeeRepository)
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Guid> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByIdAsync(request.Id);
        if (employee == null)
            throw new Exception($"Employee with id {request.Id} not found");

        employee.Update(
            request.Name,
            request.HireDate,
            request.DepartmentId,
            request.OccupationId,
            request.GradeId
            );

        await _employeeRepository.UpdateAsync(employee);
        return request.Id;
    }
}

