
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;

    

    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;

    }

    public Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(request.Name);
        foreach (var occupationId in request.OccupationIds)
        {
            var deptOcc = new DepartmentOccupation(department.Id, occupationId);
            department.AddDepartmentOccupation(deptOcc);
        }
        _departmentRepository.Add(department);
        return Task.FromResult(department.Id);
    }
}
