namespace EmployeeProfile.Application.DTOs;

public class OccupationDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DepartmentDTO department { get; set; }


}

