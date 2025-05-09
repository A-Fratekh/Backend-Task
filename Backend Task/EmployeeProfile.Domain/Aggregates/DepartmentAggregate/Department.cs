using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

public class Department :Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public ICollection<Occupation> Occupations { get; private set; }

    private Department() { } 

    public Department(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Occupations = new List<Occupation>();
    }

    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}