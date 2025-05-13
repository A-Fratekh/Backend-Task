using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain;

public class OccupationGrade
{
    [ForeignKey(nameof(Occupation))]
    public Guid OccupationId { get; set; }
    [ForeignKey(nameof(Grade))]
    public Guid GradeId { get; set; }
}
