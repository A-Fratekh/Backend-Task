using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments;

public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDTO>>
{
    private readonly IQueryRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationRepository;
    public GetAllDepartmentsHandler(IQueryRepository<Department> departmentRepository, IQueryRepository<Occupation> occupationRepository)
    {
        _departmentRepository = departmentRepository;
        _occupationRepository = occupationRepository;
    }

    public async Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<DepartmentDTO>();
        var departments = await _departmentRepository.GetAllAsync(null);
        var occupations = await _occupationRepository.GetAllAsync(null);
        foreach(var department in departments)
        {
            var deptOccupations = new List<OccupationDTO>();
            foreach (var occupation in occupations)
            {
                if (occupation.DepartmentId == department.Id)
                {
                    deptOccupations.Add(new OccupationDTO
                    {
                        Id = occupation.Id,
                        Name = occupation.Name,
                        department= new DepartmentDTO
                        {
                            Id = department.Id,
                            Name = department.Name,
                        },

                    });
                }

            }
            result.Add(new DepartmentDTO
            {

                Id = department.Id,
                Name = department.Name,
                Occupations = deptOccupations
            });

        }
        return result;
    }
}

