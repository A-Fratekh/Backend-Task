using System.ComponentModel.DataAnnotations;
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;

    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        //if(!await _departmentRepository.IsNameUniqueAsync(request.Name)) throw new ValidationException("Duplicate department name.");
        var department = new Department(request.Name);
        await _departmentRepository.AddAsync(department);
        return department.Id;
    }
}
