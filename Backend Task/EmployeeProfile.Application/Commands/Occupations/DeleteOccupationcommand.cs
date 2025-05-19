using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class DeleteOccupationcommand : IRequest
{
    public Guid Id { get; set; }
    public List<Guid> GradeIds { get; set; }
    public List<Guid> DepartmentIds { get; set; }

}
