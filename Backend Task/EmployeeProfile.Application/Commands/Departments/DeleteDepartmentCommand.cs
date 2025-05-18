using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentCommand : IRequest
{
    public Guid Id { get; set; }
}
