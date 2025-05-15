using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Domain.Aggregates.GradeAggregate;
public class Grade : AggregateRoot
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<Guid> OccupationIds { get; private set; }
    private Grade() { } 

    public Grade(string name, List<Guid> occupationIds)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OccupationIds = occupationIds;
    }

    public void Update(string name, List<Guid> occupationIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OccupationIds = occupationIds;

    }
}