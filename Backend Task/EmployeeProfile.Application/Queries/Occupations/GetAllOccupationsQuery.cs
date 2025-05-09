using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using MediatR;

namespace EmployeeProfile.Application.Queries.Occupations;

public class GetAllOccupationsQuery : IRequest<List<OccupationDTO>>
{
}
