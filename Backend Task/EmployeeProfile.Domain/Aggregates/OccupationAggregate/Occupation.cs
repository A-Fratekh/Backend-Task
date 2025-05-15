using System.ComponentModel.DataAnnotations;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;


namespace EmployeeProfile.Domain.Aggregates.OccupationAggregate;

public class Occupation : AggregateRoot
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<Guid> DepartmentIds { get; private set; }

    public List <OccupationGrade> OccupationGrades { get; private set; }
    public List<Guid> GradeIds { get; private set; }

    private Occupation() { }

    public Occupation(string name, List<Guid> departmentIds, List<Guid> gradeIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentIds = departmentIds;
        OccupationGrades = new List<OccupationGrade>();
        GradeIds = gradeIds;
    }

    public void Update(string name, List<Guid> departmentIds, List<Guid> gradeIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentIds = departmentIds;
        OccupationGrades = new List<OccupationGrade>();
        GradeIds = gradeIds;
    }

}