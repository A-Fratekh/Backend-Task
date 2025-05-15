using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetGradeQuery : IRequest<GradeDTO>
{
    public Guid Id { get; set; }
}
