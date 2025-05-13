using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;

namespace EmployeeProfile.Domain.Aggregates.OccupationAggregate;

public class Occupation : Entity, IAggregateRoot
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public ICollection<OccupationGrade> Grades { get; set; }

    private Occupation() { }

    public Occupation(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Grades = new HashSet<OccupationGrade>();
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

    }

}