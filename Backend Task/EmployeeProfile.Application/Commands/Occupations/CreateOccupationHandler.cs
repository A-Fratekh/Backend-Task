using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationHandler : IRequestHandler<CreateOccupationCommand, Guid>
{
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository, 
        IQueryRepository<Department> departmentQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name, request.DepartmentIds);
        await _occupationCommandRepository.AddAsync(occupation);

        foreach (var departmentId in request.DepartmentIds)
        {
            occupation.DepartmentOccupations.Add(new DepartmentOccupation(departmentId, occupation.Id));
        }

        var departments = await _departmentQueryRepository.GetAllAsync(null);
        occupation.DepartmentOccupations.ForEach(async d => {
            var department = await _departmentQueryRepository.GetByIdAsync(d.DepartmentId);
            department.OccupationIds.Add(occupation.Id);
            department.Update(department.Name, department.OccupationIds);
            await _departmentRepository.UpdateAsync(department);
        });
           
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return occupation.Id;
    }
}
