using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class DeleteGradeHandler : IRequestHandler<DeleteGradeCommand, Guid>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepositor;
    private readonly ICommandRepository<Grade> _gradeCommandRepositor;

    public DeleteGradeHandler(IQueryRepository<Grade> gradeQueryRepositor, ICommandRepository<Grade> gradeCommandRepositor)
    {
        _gradeQueryRepositor = gradeQueryRepositor;
        _gradeCommandRepositor = gradeCommandRepositor;
    }

    public async Task<Guid> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepositor.GetByIdAsync(request.Id);
        if (grade == null) {
            throw new ArgumentException($"Grade with id {request.Id} couldn't be found");
        }
        await _gradeCommandRepositor.DeleteAsync(grade);
        return request.Id;

    }
}
