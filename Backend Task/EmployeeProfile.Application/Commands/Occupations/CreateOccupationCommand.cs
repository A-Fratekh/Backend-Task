﻿using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public List<Guid> DepartmentIds { get; set; }
    public List <Guid> GradeIds { get; set; }
}
