using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;
namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentCommand :IRequest<Guid>
{
    public string Name { get; set; }
    public ICollection<Occupation> Occupations { get; set; }
}
