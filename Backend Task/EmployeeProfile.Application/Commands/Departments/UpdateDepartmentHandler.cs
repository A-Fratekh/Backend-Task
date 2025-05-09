
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
namespace EmployeeProfile.Application.Commands.Departments;

public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;

    public UpdateDepartmentHandler(ICommandRepository<Department> departmentRepository, IQueryRepository<Department> departmentQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
    }

    public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");

        department.Update(request.Name);
        await _departmentRepository.UpdateAsync(department);
        return request.Id;
    }
}
