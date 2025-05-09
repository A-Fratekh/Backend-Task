
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.DTOs;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;

    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(request.Name);
        await _departmentRepository.AddAsync(department);
        return department.Id;
    }
}
