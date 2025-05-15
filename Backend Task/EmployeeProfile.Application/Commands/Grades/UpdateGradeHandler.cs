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

public class UpdateGradeHandler : IRequestHandler<UpdateGradeCommand, Guid>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly ICommandRepository<Grade> _gradeCommandRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGradeHandler(IQueryRepository<Grade> gradeQueryRepository,
        ICommandRepository<Grade> gradeCommandRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _gradeQueryRepository = gradeQueryRepository;
        _gradeCommandRepository = gradeCommandRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepository.GetByIdAsync(request.Id);
        grade=grade??throw new ArgumentException($"Grade with id : {request.Id} couldn't be found");
        
        grade.Update(request.Name, request.OccupationIds);
        await _gradeCommandRepository.UpdateAsync(grade);
        foreach (var occupationId in grade.OccupationIds) {
            var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
            if (!occupation.GradeIds.Contains(grade.Id))
            {
                occupation.GradeIds.Add(occupation.Id);
                occupation.Update(occupation.Name,occupation.DepartmentIds, occupation.GradeIds);
                await _occupationCommandRepository.UpdateAsync(occupation);
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return grade.Id;

    }
}
