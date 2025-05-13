using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
}
