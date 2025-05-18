
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationRepository;
    

    public CreateDepartmentHandler(ICommandRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationRepository)
    {
        _departmentRepository = departmentRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationRepository = occupationRepository;
    }

    public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Department(request.Name, request.OccupationIds);
        foreach (var occupationId in department.OccupationIds)
        {
            var deptOcc = new DepartmentOccupation(department.Id, occupationId);
            department.AddDepartmentOccupation(deptOcc);
        }
        await _departmentRepository.AddAsync(department);
        foreach (var occupationId in department.OccupationIds)
        {
            var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
            occupation.DepartmentIds.Add(department.Id);
            occupation.Update(occupation.Name, occupation.DepartmentIds, occupation.GradeIds);
            await _occupationRepository.UpdateAsync(occupation);
        }
    }
}
