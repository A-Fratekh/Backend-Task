using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments;

public class GetDepartmentByIdQuery : IRequest<DepartmentDTO>
{
    public Guid Id { get; set; }
}
