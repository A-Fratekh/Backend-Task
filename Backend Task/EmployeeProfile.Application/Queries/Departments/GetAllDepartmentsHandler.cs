using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments;

public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<Department>>
{
    private readonly IQueryRepository<Department> _departmentRepository;

    public GetAllDepartmentsHandler(IQueryRepository<Department> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<List<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(null);
        return departments.ToList();
    }
}

