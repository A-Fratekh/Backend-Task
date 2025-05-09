using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class UpdateOccupationCommand: IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
