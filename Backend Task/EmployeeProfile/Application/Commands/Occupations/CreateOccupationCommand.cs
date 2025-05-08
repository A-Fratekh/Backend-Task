using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public Guid DepartmentId { get; set; }    
}
