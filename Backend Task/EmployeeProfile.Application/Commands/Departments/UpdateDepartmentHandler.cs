
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
namespace EmployeeProfile.Application.Commands.Departments;

public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;

    public UpdateDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Occupation> occupationQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }

    public Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department =  _departmentQueryRepository.GetById(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");
        foreach (var occupationId in request.OccupationIds)
        {
            if (!department.DepartmentOccupations.Any().Equals(new DepartmentOccupation(department.Id, occupationId)))
            {
                var deptOcc = new DepartmentOccupation(department.Id, occupationId);
                department.AddDepartmentOccupation(deptOcc);
            }
        }
        department.Update(request.Name);
        _departmentRepository.Update(department);
        var occupations = _occupationQueryRepository.GetAll(null);
        return Task.CompletedTask;
    }
}
