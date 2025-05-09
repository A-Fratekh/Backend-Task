using EmployeeProfile.Application.DTOs;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeesListQuery : IRequest<List<EmployeeProfileDTO>>
{
    public string OrderBy { get; set; } = "HireDate";
}
