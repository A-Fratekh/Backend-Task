﻿using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class CreateGradeCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public List<Guid> OccupationIds { get; set; }
}
