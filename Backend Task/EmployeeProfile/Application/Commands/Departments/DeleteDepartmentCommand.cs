using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}
