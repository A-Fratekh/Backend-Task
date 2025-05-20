using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;

namespace EmployeeProfile.Domain.Aggregates.OccupationAggregate;

public class OccupationGrade
{
    [ForeignKey(nameof(Occupation))]
    public Guid OccupationId { get; set; }
    [ForeignKey(nameof(Grade))]
    public Guid GradeId { get; set; }


    public OccupationGrade(Guid occupationId, Guid gradeId)
    {
        OccupationId = occupationId;
        GradeId = gradeId;
    }
}
