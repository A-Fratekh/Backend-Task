using EmployeeProfile.Application.DTOs;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentDTO>>
    {
    }
}
