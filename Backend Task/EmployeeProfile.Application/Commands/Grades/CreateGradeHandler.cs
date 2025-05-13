using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades
{
    public class CreateGradeHandler : IRequestHandler<CreateGradeCommand, Guid>
    {
        private readonly ICommandRepository<Grade> _gradeCommandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGradeHandler(ICommandRepository<Grade> gradeCommandRepository,
            IUnitOfWork unitOfWork)
        {
            _gradeCommandRepository = gradeCommandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = new Grade(request.Name, request.OccupationId);
            await _gradeCommandRepository.AddAsync(grade);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return grade.Id;
        }
    }
}
