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
    private readonly List<OccupationGrade> _occupationGrades = [];
    public virtual IReadOnlyList<OccupationGrade> OccupationGrades => _occupationGrades;
    public List<Guid> GradeIds { get; private set; }

    private Occupation() { }

    public Occupation(string name, List<Guid> departmentIds, List<Guid> gradeIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentIds = departmentIds;
        GradeIds = gradeIds;
    }

    public void AddOccupationGrade(OccupationGrade occGrade)
    {
        _occupationGrades.Add(occGrade);
    }
    public void RemoveOccupationGrade(OccupationGrade occGrade)
    {
        _occupationGrades.Remove(occGrade);
    }

    public void Update(string name, List<Guid> departmentIds, List<Guid> gradeIds)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        DepartmentIds = departmentIds;
        GradeIds = gradeIds;
    }

}