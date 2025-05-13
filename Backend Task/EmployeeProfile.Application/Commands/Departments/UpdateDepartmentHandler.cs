
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.UnitOfWork;
namespace EmployeeProfile.Application.Commands.Departments;

public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDepartmentHandler(ICommandRepository<Department> departmentRepository, 
        IQueryRepository<Department> departmentQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");

        department.Update(request.Name);
        await _departmentRepository.UpdateAsync(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
