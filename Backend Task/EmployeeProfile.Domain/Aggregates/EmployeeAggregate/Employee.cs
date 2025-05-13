using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;


namespace EmployeeProfile.Domain.Aggregates.EmployeeAggregate;

public class Employee : AggregateRoot
{
    [Key]
    public int EmployeeNo { get; private set; }
   
    public string Name { get; private set; }

    public DateOnly HireDate { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid OccupationId { get; private set; }
    public Guid GradeId { get; private set; }

    private Employee() { }

    public Employee(string name, DateOnly hireDate,
        Guid departmentId, Guid occupationId, Guid gradeId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HireDate = hireDate;
        DepartmentId = departmentId;
        OccupationId = occupationId;
        GradeId = gradeId;

    }

    public void Update(string name, DateOnly hireDate,
       Guid departmentId, Guid occupationId, Guid gradeId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        HireDate = hireDate;
        DepartmentId = departmentId;
        OccupationId = occupationId;
        GradeId = gradeId;
    }
}
