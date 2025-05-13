using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class UpdateEmployeeCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid OccupationId { get; set; }
    public Guid GradeId { get; set; }
}
