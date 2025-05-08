using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;

    public DeleteDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
    }

    public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");

        await _departmentRepository.DeleteAsync(department);
        return request.Id;
    }
}
