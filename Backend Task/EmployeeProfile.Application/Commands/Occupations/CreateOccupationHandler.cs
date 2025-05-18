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

    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        ICommandRepository<Grade> gradeRepository,
        IQueryRepository<Grade> gradeQueryRepository)
    {
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeRepository = gradeRepository;
        _gradeQueryRepository = gradeQueryRepository;
    }

    public Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name, request.DepartmentIds, request.GradeIds);
         _occupationCommandRepository.Add(occupation);
        foreach (var gradeId in occupation.GradeIds)
        {
            var occGrade = new OccupationGrade(occupation.Id, gradeId);
            occupation.AddOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
        }
        foreach (var departmentId in occupation.DepartmentIds)
        {
            var department =  _departmentQueryRepository.GetById(departmentId);
            department.OccupationIds.Add(occupation.Id);
            department.Update(department.Name, department.OccupationIds);
            department.AddDepartmentOccupation(new DepartmentOccupation(departmentId, occupation.Id));
             _departmentRepository.Update(department);
        }

        foreach (var gradeId in occupation.GradeIds)
        {
            var grade =  _gradeQueryRepository.GetById(gradeId);
            grade.OccupationIds.Add(occupation.Id);
            grade.Update(grade.Name, grade.OccupationIds);
             _gradeRepository.Update(grade);
        }
        return Task.FromResult(occupation.Id);
    }
}
