﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class DeleteGradeCommand : IRequest
{
   public Guid Id { get; set; }
    public List<Guid> OccupationIds { get; set; }
}
