using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class DeleteGradeHandler : IRequestHandler<DeleteGradeCommand, Guid>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepositor;
    private readonly ICommandRepository<Grade> _gradeCommandRepositor;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGradeHandler(IQueryRepository<Grade> gradeQueryRepositor,
        ICommandRepository<Grade> gradeCommandRepositor,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _gradeQueryRepositor = gradeQueryRepositor;
        _gradeCommandRepositor = gradeCommandRepositor;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepositor.GetByIdAsync(request.Id) ?? throw new ArgumentException($"Grade with id {request.Id} couldn't be found");

        foreach(var occupationId in grade.OccupationIds)
        {
            var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
            occupation.RemoveOccupationGrade(new OccupationGrade(occupationId, grade.Id));
            occupation.GradeIds.Remove(grade.Id);
            occupation.Update(occupation.Name, occupation.DepartmentIds, occupation.GradeIds);
            await _occupationCommandRepository.UpdateAsync(occupation);
        }
        await _gradeCommandRepositor.DeleteAsync(grade);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;

    }
}
