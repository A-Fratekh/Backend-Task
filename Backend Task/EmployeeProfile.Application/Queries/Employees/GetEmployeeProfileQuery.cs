﻿using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeeProfileQuery :IRequest<Employee>
{
    public Guid EmployeeId { get; set; }
}
