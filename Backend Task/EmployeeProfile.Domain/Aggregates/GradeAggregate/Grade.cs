using System.ComponentModel.DataAnnotations;

namespace EmployeeProfile.Domain.Aggregates.GradeAggregate;
public class Grade : AggregateRoot
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Grade() { } 

    public Grade(string name)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

    }
}