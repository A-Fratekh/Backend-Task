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

        public CreateGradeHandler(ICommandRepository<Grade> gradeCommandRepository,
            ICommandRepository<Occupation> occupationCommandRepository, 
            IQueryRepository<Occupation> occupationQueryRepository)
        {
            _gradeCommandRepository = gradeCommandRepository;
            _occupationCommandRepository = occupationCommandRepository;
            _occupationQueryRepository = occupationQueryRepository;
        }

        public Task<Guid> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = new Grade(request.Name, request.OccupationIds);
            
             _gradeCommandRepository.Add(grade);
            foreach(var occupationId in grade.OccupationIds)
            {
                var occupation =_occupationQueryRepository.GetById(occupationId);
                occupation.GradeIds.Add(grade.Id);
                occupation.Update(occupation.Name,occupation.DepartmentIds, occupation.GradeIds);
                occupation.AddOccupationGrade(new OccupationGrade(occupationId, grade.Id));
                _occupationCommandRepository.Update(occupation);
            }
            return Task.FromResult(grade.Id);
        }
    }
}
