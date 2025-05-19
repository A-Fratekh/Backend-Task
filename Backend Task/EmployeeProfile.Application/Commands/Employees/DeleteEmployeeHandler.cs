using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IQueryRepository<Employee> _employeeReadRepository;
    private readonly ICommandRepository<Employee> _employeeRepository;

    public DeleteEmployeeHandler(IQueryRepository<Employee> employeeReadRepository
            , ICommandRepository<Employee> employeeRepository
             )
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
    }

    public Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee =  _employeeReadRepository.GetById(request.EmployeeNo);
        if (employee == null)
            throw new Exception($"Employee with id {request.EmployeeNo} not found");
         _employeeRepository.Delete(employee);
        return Task.CompletedTask;
    }
}
