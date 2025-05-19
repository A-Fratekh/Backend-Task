using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;

    public DeleteDepartmentHandler(
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Occupation> occupationQueryRepository)
    {
        _departmentRepository      = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }

    public  Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _departmentQueryRepository.GetById(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");
        foreach (var occupationId in request.OccupationIds)
        {
            department.RemoveDepartmentOccupation(new DepartmentOccupation(department.Id, occupationId));;
        }
        _departmentRepository.Delete(department);
        return Task.CompletedTask;
    }
}
