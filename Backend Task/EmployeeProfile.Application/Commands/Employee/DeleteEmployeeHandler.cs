using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, int>
{
    private readonly IQueryRepository<Employee> _employeeReadRepository;
    private readonly ICommandRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeHandler(IQueryRepository<Employee> employeeReadRepository
            , ICommandRepository<Employee> employeeRepository
            , IUnitOfWork unitOfWork  )
    {
        _employeeReadRepository = employeeReadRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByIdAsync(request.Id);
        if (employee == null)
            throw new Exception($"Employee with id {request.Id} not found");

        await _employeeRepository.DeleteAsync(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
