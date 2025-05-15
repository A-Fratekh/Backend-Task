using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;

namespace EmployeeProfile.Application.DTOs;

public class OccupationDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Guid> DepartmentIds { get; set; }
    public List<string> Departments { get; set; }

    public ICollection<Guid> GradeIds { get; set; }
    public List<string> Grades { get; set; }
}

