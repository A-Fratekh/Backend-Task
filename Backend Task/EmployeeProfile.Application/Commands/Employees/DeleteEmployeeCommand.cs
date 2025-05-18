using MediatR;

namespace EmployeeProfile.Application.Commands.Employees;

public class DeleteEmployeeCommand : IRequest
{
    public int EmployeeNo { get; set; }
}
