using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades
{
    public class CreateGradeHandler : IRequestHandler<CreateGradeCommand, Guid>
    {
        private readonly ICommandRepository<Grade> _gradeCommandRepository;

        public CreateGradeHandler(ICommandRepository<Grade> gradeCommandRepository)
        {
            _gradeCommandRepository = gradeCommandRepository;
        }

        public async Task<Guid> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = new Grade(request.Name, request.OccupationId);
            await _gradeCommandRepository.AddAsync(grade);
            return grade.Id;
        }
    }
}
