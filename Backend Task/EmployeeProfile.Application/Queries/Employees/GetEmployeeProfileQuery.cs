using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeeProfileQuery :IRequest<EmployeeProfileDTO>
{
    public int EmployeeNo { get; set; }
}
