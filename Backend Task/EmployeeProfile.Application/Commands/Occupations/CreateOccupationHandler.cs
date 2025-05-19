using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationHandler : IRequestHandler<CreateOccupationCommand, Guid>
{
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;


    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository,
        IQueryRepository<Department> departmentQueryRepository)
    {
        _occupationCommandRepository = occupationCommandRepository;
        _departmentQueryRepository = departmentQueryRepository;

    }

    public Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name);
         _occupationCommandRepository.Add(occupation);
        foreach (var gradeId in request.GradeIds)
        {
            var occGrade = new OccupationGrade(occupation.Id, gradeId);
            occupation.AddOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
        }
        foreach (var departmentId in request.DepartmentIds)
        {
            var department =  _departmentQueryRepository.GetById(departmentId);
            department.AddDepartmentOccupation(new DepartmentOccupation(departmentId, occupation.Id));
        }
        return Task.FromResult(occupation.Id);
    }
}
