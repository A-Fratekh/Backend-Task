using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

namespace EmployeeProfile.Domain.Aggregates.OccupationAggregate;

public class Occupation : Entity, IAggregateRoot
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }

    private Occupation() { }

    public Occupation(string name, Guid departmentId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentId = departmentId;
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

}