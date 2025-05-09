using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class CreateGradeCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public Guid OccupationId { get; set; }
}
