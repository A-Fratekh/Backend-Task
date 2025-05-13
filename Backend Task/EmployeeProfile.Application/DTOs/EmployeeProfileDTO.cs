using System.Text.Json.Serialization;


namespace EmployeeProfile.Application.DTOs;

public class EmployeeProfileDTO
{
    public int EmployeeNo { get; set; }
    public string Name { get; set; }
    public DateOnly HireDate { get; set; }
    public string Department { get; set; }
    public string Occupation { get; set; }
    public string Grade { get; set; }

}
