using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

public class Department : AggregateRoot
{
    [Key]
    public Guid Id { get; private set; }   
    public string Name { get; private set; }

    private readonly List<DepartmentOccupation> _departmentOccupations = [];
    public virtual IReadOnlyList<DepartmentOccupation> DepartmentOccupations => _departmentOccupations;


    private Department() { } 

    public Department(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    public void AddDepartmentOccupation (DepartmentOccupation occupation)
    {
        _departmentOccupations.Add(occupation);
    }

    public void RemoveDepartmentOccupation(DepartmentOccupation occupation)
    {
        _departmentOccupations.Remove(occupation);
    }
    public void Update(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}