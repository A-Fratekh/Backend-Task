
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationRepository,
        IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationRepository = occupationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(request.Name, request.OccupationIds);
        await _departmentRepository.AddAsync(department);
        foreach (var occupationId in department.OccupationIds)
        {
            var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
            occupation.DepartmentIds.Add(department.Id);
            occupation.Update(occupation.Name, occupation.DepartmentIds);
            await _occupationRepository.UpdateAsync(occupation);
        }
        foreach (var occupationId in department.OccupationIds)
        {
            department.DepartmentOccupations.Add(new DepartmentOccupation(department.Id, occupationId));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return department.Id;
    }
}
