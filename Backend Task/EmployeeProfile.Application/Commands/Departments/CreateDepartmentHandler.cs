
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Application.UnitOfWork;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(request.Name);
        await _departmentRepository.AddAsync(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return department.Id;
    }
}
