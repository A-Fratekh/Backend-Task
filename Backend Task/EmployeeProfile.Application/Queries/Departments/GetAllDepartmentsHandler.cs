using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments;

public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDTO>>
{
    private readonly IQueryRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    public GetAllDepartmentsHandler(IQueryRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }

    public Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDTO> result = new List<DepartmentDTO>();
        var departments =  _departmentRepository.GetAll(null);
        
        foreach(var department in departments)
        {
            var occupations = new List<string>();
            foreach (var occupationId in department.OccupationIds)
            {
                var occupation = _occupationQueryRepository.GetById(occupationId);
                occupations.Add(occupation.Name);
            }
                result.Add(new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                OccupationIds = department.OccupationIds,
                Occupations = occupations
            });

        }
        return Task.FromResult(result);
    }
}

