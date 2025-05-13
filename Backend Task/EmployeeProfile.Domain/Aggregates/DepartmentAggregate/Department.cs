using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

public class Department : AggregateRoot
{
    [Key]
    public Guid Id { get; set; }   
    public string Name { get; private set; }


    private Department() { } 

    public Department(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}