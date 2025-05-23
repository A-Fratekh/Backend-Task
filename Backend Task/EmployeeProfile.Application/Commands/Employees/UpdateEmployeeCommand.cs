﻿using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class UpdateEmployeeCommand : IRequest
{
    public int EmployeeNo { get; set; }
    public string Name { get; set; }
    public DateOnly HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid OccupationId { get; set; }
    public Guid GradeId { get; set; }
}
