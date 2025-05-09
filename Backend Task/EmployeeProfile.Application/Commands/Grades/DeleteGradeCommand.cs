using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Grades;

public class DeleteGradeCommand : IRequest<Guid>
{
   public Guid Id { get; set; }
}
