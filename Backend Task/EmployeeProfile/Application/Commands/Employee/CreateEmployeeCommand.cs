using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeCommand : IRequest<Guid>
{
    public string EmployeeNo { get; set; }
    public string Name { get; set; }
    public DateTime HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid OccupationId { get; set; }
    public Guid GradeId { get; set; }
}
