using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Occupations;

public class GetAllOccupationsHandler : IRequestHandler<GetAllOccupationsQuery, List<OccupationDTO>>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;



    public GetAllOccupationsHandler(IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Grade> gradeQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeQueryRepository = gradeQueryRepository;
    }

    public Task<List<OccupationDTO>> Handle(GetAllOccupationsQuery request, CancellationToken cancellationToken)
    {
        var occupations = _occupationQueryRepository.GetAll(null);
        var result = new List<OccupationDTO>();

        foreach(var occupation in occupations)
        {
            var departments = new List<string>();
            foreach (var departmentId in occupation.DepartmentIds)
            {
                var department =  _departmentQueryRepository.GetById(departmentId);
                departments.Add(department.Name);
            }
            var grades = new List<string>();
            foreach (var gradeId in occupation.GradeIds)
            {
                var grade = _gradeQueryRepository.GetById(gradeId);
                grades.Add(grade.Name);
            }
            result.Add(new OccupationDTO
            {
                Id = occupation.Id,
                Name = occupation.Name,
                DepartmentIds = occupation.DepartmentIds,
                Departments = departments,
                GradeIds = occupation.GradeIds,
                Grades = grades
            });
        }

        return Task.FromResult(result);
    }
}