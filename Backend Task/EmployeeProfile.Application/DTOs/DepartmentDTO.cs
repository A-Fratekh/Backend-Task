using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Application.DTOs;

public class DepartmentDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<string> Occupations { get; set; }
}
