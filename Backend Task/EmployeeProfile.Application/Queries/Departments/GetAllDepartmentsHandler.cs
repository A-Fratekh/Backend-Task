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
    private readonly IQueryRepository<DepartmentOccupation> _deptOccQueryRepository;

    public GetAllDepartmentsHandler(IQueryRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<DepartmentOccupation> deptOccQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _deptOccQueryRepository = deptOccQueryRepository;
    }

    public Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentDTO> result = new List<DepartmentDTO>();
        var departments =  _departmentRepository.GetAll(null);
        var deptOccs = _deptOccQueryRepository.GetAll(null);
        
       
        foreach (var department in departments)
        {
            var occupations = new List<string>();
            foreach (var deptOcc in deptOccs)
            {
                if (deptOcc.DepartmentId == department.Id)
                {
                    var occupation = _occupationQueryRepository.GetById(deptOcc.OccupationId);
                    occupations.Add(occupation.Name);
                }
            }
            result.Add(new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Occupations = occupations
            });
        }
        return Task.FromResult(result);
    }
}