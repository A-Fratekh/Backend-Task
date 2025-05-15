using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Application.DTOs;

public class GradeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection <Guid> OccupationIds { get; set; }
    public List<string> Occupations { get; set; }

}
