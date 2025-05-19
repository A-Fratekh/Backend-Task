
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class UpdateOccupationHandler : IRequestHandler<UpdateOccupationCommand>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Grade> _gradeRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;

    public UpdateOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        ICommandRepository<Grade> gradeRepository,
        IQueryRepository<Grade> gradeQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeRepository = gradeRepository;
        _gradeQueryRepository = gradeQueryRepository;
    }

    public  Task Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = _occupationQueryRepository.GetById(request.Id);
        occupation = occupation?? throw new ArgumentNullException($"Occupation with Id : {request.Id} couldn't be found");
        occupation.Update(request.Name);
        foreach (var gradeId in request.GradeIds)
        {
           occupation.AddOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
        }
        _occupationCommandRepository.Update(occupation);
        return Task.CompletedTask;
    }
}
