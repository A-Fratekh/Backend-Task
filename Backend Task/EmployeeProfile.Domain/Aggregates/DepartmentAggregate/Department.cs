using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

public class Department :Entity, IAggregateRoot
{
    [Key]
    public Guid Id { get; set; }   
    public string Name { get; private set; }
    public ICollection<DepartmentOccupation> Occupations { get; private set; }


    private Department() { } 

    public Department(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Occupations = new List<DepartmentOccupation>();
    }

    public void Update(string name, ICollection<DepartmentOccupation> occupations)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Occupations = occupations;
    }
}