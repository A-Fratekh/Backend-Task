
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
namespace EmployeeProfile.Application.Commands.Departments;

public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationRepository;

    public UpdateDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationRepository = occupationRepository;
    }

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");

        department.Update(request.Name, request.OccupationIds);
        await _departmentRepository.UpdateAsync(department);
        var occupations = await _occupationQueryRepository.GetAllAsync(null);
        foreach (var occupationId in department.OccupationIds)
        {
            var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
            if (!occupation.DepartmentIds.Contains(department.Id))
            { 
                occupation.DepartmentIds.Add(department.Id);
                occupation.Update(occupation.Name, occupation.DepartmentIds, occupation.GradeIds);
                await _occupationRepository.UpdateAsync(occupation);
            }
        }

    }
}
