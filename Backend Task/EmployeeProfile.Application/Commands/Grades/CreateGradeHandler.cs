using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades
{
    public class CreateGradeHandler : IRequestHandler<CreateGradeCommand, Guid>
    {
        private readonly ICommandRepository<Grade> _gradeCommandRepository;
        private readonly ICommandRepository<Occupation> _occupationCommandRepository;
        private readonly IQueryRepository<Occupation> _occupationQueryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGradeHandler(ICommandRepository<Grade> gradeCommandRepository,
            ICommandRepository<Occupation> occupationCommandRepository, 
            IQueryRepository<Occupation> occupationQueryRepository, 
            IUnitOfWork unitOfWork)
        {
            _gradeCommandRepository = gradeCommandRepository;
            _occupationCommandRepository = occupationCommandRepository;
            _occupationQueryRepository = occupationQueryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = new Grade(request.Name, request.OccupationIds);
            
            await _gradeCommandRepository.AddAsync(grade);
            foreach(var occupationId in grade.OccupationIds)
            {
                var occupation =await _occupationQueryRepository.GetByIdAsync(occupationId);
                occupation.GradeIds.Add(grade.Id);
                occupation.Update(occupation.Name,occupation.DepartmentIds, occupation.GradeIds);
                occupation.AddOccupationGrade(new OccupationGrade(occupationId, grade.Id));
                await _occupationCommandRepository.UpdateAsync(occupation);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return grade.Id;
        }
    }
}
