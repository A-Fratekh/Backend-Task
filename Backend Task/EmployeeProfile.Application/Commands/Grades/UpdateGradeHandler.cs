using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class UpdateGradeHandler : IRequestHandler<UpdateGradeCommand, Guid>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepositor;
    private readonly ICommandRepository<Grade> _gradeCommandRepositor;

    public UpdateGradeHandler(IQueryRepository<Grade> gradeQueryRepositor, ICommandRepository<Grade> gradeCommandRepositor)
    {
        _gradeQueryRepositor = gradeQueryRepositor;
        _gradeCommandRepositor = gradeCommandRepositor;
    }
    public async Task<Guid> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepositor.GetByIdAsync(request.Id);
        if (grade == null) {
            throw new ArgumentException($"Grade with id : {request.Id} couldn't be found");
        }
        grade.Update(
            request.Name
            );
        await _gradeCommandRepositor.UpdateAsync(grade);
        return grade.Id;

    }
}
