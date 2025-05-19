
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class UpdateGradeHandler : IRequestHandler<UpdateGradeCommand>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly ICommandRepository<Grade> _gradeCommandRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;

    public UpdateGradeHandler(IQueryRepository<Grade> gradeQueryRepository,
        ICommandRepository<Grade> gradeCommandRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
        _gradeCommandRepository = gradeCommandRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
    }

    public Task Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade =  _gradeQueryRepository.GetById(request.Id);
        grade=grade??throw new ArgumentException($"Grade with id : {request.Id} couldn't be found");
        grade.Update(request.Name);
        _gradeCommandRepository.Update(grade);
        foreach (var occupationId in request.OccupationIds) {
            var occupation = _occupationQueryRepository.GetById(occupationId);
                occupation.AddOccupationGrade(new OccupationGrade(occupationId, grade.Id));
                _occupationCommandRepository.Update(occupation);
        }
        return Task.CompletedTask;
    }
}
