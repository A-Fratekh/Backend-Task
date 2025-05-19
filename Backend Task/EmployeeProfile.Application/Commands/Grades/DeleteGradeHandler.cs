
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class DeleteGradeHandler : IRequestHandler<DeleteGradeCommand>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepositor;
    private readonly ICommandRepository<Grade> _gradeCommandRepositor;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;

    public DeleteGradeHandler(IQueryRepository<Grade> gradeQueryRepositor,
        ICommandRepository<Grade> gradeCommandRepositor,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository)
    {
        _gradeQueryRepositor = gradeQueryRepositor;
        _gradeCommandRepositor = gradeCommandRepositor;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
    }

    public Task Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        var grade =  _gradeQueryRepositor.GetById(request.Id) ?? throw new ArgumentException($"Grade with id {request.Id} couldn't be found");

        foreach(var occupationId in request.OccupationIds)
        {
            var occupation = _occupationQueryRepository.GetById(occupationId);
            occupation.RemoveOccupationGrade(new OccupationGrade(occupationId, grade.Id));
             _occupationCommandRepository.Update(occupation);
        }
        _gradeCommandRepositor.Delete(grade);
        return Task.CompletedTask;

    }
}
