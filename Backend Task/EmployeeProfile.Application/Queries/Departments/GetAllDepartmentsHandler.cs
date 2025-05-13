using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments;

public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDTO>>
{
    private readonly IQueryRepository<Department> _departmentRepository;
    public GetAllDepartmentsHandler(IQueryRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<DepartmentDTO>();
        var departments = await _departmentRepository.GetAllAsync(null);
        foreach(var department in departments)
        {
            result.Add(new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Occupations = department.Occupations,
            });

        }
        return result;
    }
}

