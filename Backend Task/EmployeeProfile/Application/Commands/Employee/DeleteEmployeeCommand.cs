using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}
