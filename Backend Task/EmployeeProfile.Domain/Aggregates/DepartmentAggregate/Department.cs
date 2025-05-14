using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain.Aggregates.DepartmentAggregate;

public class Department : AggregateRoot
{
    [Key]
    public Guid Id { get; private set; }   
    public string Name { get; private set; }
    public List<Guid> OccupationIds { get; private set; }
    public List<DepartmentOccupation> DepartmentOccupations { get; private set; }


    private Department() { } 

    public Department(string name, List<Guid> occupationIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentOccupations = new List<DepartmentOccupation>();
        OccupationIds= occupationIds;
    }
   

    public void Update(string name, List<Guid> occupationIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentOccupations = new List<DepartmentOccupation>();
        OccupationIds=occupationIds;
    }
}