using MediatR;
namespace EmployeeProfile.Application.Commands.Departments;
public class CreateDepartmentCommand :IRequest<Guid>
{
    public string Name { get; set; }


}
