using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Queries.Occcupations;

public class GetOccupationQuery : IRequest<OccupationDTO>
{
    public Guid Id { get; set; }
}
