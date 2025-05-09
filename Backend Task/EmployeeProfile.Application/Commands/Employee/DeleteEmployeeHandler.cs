using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Guid>
{
    private readonly IQueryRepository<Employee> _employeeReadRepository;
    private readonly ICommandRepository<Employee> _employeeRepository;

    public DeleteEmployeeHandler(IQueryRepository<Employee> employeeReadRepository, ICommandRepository<Employee> employeeRepository)
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Guid> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByIdAsync(request.Id);
        if (employee == null)
            throw new Exception($"Employee with id {request.Id} not found");

        await _employeeRepository.DeleteAsync(employee);
        return request.Id;
    }
}
