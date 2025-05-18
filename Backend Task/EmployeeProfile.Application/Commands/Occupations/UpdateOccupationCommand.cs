using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class UpdateOccupationCommand: IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> DepartmentIds { get; set; }
    public List<Guid> GradeIds { get; set; }

}
