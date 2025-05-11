namespace EmployeeProfile.Application.DTOs;

public class EmployeeProfileDTO
{
    public string EmployeeNo { get; set; }
    public string Name { get; set; }
    public DateTime HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public Guid OccupationId { get; set; }
    public string Occupation { get; set; }
    public Guid GradeId { get; set; }
    public string Grade { get; set; }

}
