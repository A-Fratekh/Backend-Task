﻿namespace EmployeeProfile.Application.DTOs;

public class GradeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OccupationId { get; set; }
    public OccupationDTO Occupation { get; set; }
}
