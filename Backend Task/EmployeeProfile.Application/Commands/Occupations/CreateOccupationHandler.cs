using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationHandler : IRequestHandler<CreateOccupationCommand, Guid>
{
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Grade> _gradeRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        ICommandRepository<Grade> gradeRepository,
        IQueryRepository<Grade> gradeQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeRepository = gradeRepository;
        _gradeQueryRepository = gradeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name, request.DepartmentIds, request.GradeIds);
        await _occupationCommandRepository.AddAsync(occupation);
        foreach (var gradeId in occupation.GradeIds)
        {
            var occGrade = new OccupationGrade(occupation.Id, gradeId);
            occupation.AddOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
        }
        foreach (var departmentId in occupation.DepartmentIds)
        {
            var department = await _departmentQueryRepository.GetByIdAsync(departmentId);
            department.OccupationIds.Add(occupation.Id);
            department.Update(department.Name, department.OccupationIds);
            department.AddDepartmentOccupation(new DepartmentOccupation(departmentId, occupation.Id));
            await _departmentRepository.UpdateAsync(department);
        }

        foreach (var gradeId in occupation.GradeIds)
        {
            var grade = await _gradeQueryRepository.GetByIdAsync(gradeId);
            grade.OccupationIds.Add(occupation.Id);
            grade.Update(grade.Name, grade.OccupationIds);
            await _gradeRepository.UpdateAsync(grade);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return occupation.Id;
    }
}
