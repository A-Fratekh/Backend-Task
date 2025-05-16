using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentHandler(ICommandRepository<Department> departmentRepository, IQueryRepository<Department> departmentQueryRepository, ICommandRepository<Occupation> occupationRepository, IQueryRepository<Occupation> occupationQueryRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationRepository = occupationRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");
        var occupations = await _occupationQueryRepository.GetAllAsync(null);
        foreach (var occupation in occupations)
        {
            department.RemoveDepartmentOccupation(new DepartmentOccupation(department.Id, occupation.Id));
            occupation.DepartmentIds.Remove(department.Id);
            occupation.Update(occupation.Name, occupation.DepartmentIds, occupation.GradeIds);
            await _occupationRepository.UpdateAsync(occupation);
        }
        await _departmentRepository.DeleteAsync(department);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
