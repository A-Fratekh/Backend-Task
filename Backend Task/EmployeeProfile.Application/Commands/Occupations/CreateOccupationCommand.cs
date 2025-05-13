using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public ICollection<Guid> Departments { get; set; } 
    public ICollection<Grade> Grades { get; set; }
}
