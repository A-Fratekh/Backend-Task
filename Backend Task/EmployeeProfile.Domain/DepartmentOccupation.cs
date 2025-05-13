using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;

namespace EmployeeProfile.Domain;

public class DepartmentOccupation
{
    [ForeignKey(nameof(Department))]
    public Guid DepartmentId { get; set; }

    [ForeignKey(nameof(Occupation))]
    public Guid OccupationId { get; set; }

    public Department Department { get; set; }
    public Occupation Occupation { get; set; }

}
