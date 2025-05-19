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
    private readonly IQueryRepository<DepartmentOccupation> _deptOccQueryRepository;
    private readonly IQueryRepository<OccupationGrade> _occGradeQueryRepository;

    public GetAllOccupationsHandler(
        IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Grade> gradeQueryRepository,
        IQueryRepository<DepartmentOccupation> deptOccQueryRepository,
        IQueryRepository<OccupationGrade> occGradeQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeQueryRepository = gradeQueryRepository;
        _deptOccQueryRepository = deptOccQueryRepository;
        _occGradeQueryRepository = occGradeQueryRepository;
    }

    public Task<List<OccupationDTO>> Handle(GetAllOccupationsQuery request, CancellationToken cancellationToken)
    {
        var occupations = _occupationQueryRepository.GetAll(null);
        var result = new List<OccupationDTO>();
        var occGrades = _occGradeQueryRepository.GetAll(null);
        var deptOccs = _deptOccQueryRepository.GetAll(null);

        foreach (var occupation in occupations)
        {
            var departments = new List<string>();
            foreach (var deptOcc in deptOccs)
            {
                if (deptOcc.OccupationId == occupation.Id)
                {
                    var department = _departmentQueryRepository.GetById(deptOcc.DepartmentId);
                    departments.Add(department.Name);
                }
            }
            var grades = new List<string>();
            foreach (var occGrade in occGrades)
            {
                if (occGrade.OccupationId == occupation.Id)
                {
                    var grade = _gradeQueryRepository.GetById(occGrade.GradeId);
                    grades.Add(grade.Name);
                }
            }
            result.Add(new OccupationDTO
            {
                Id = occupation.Id,
                Name = occupation.Name,
                Departments = departments,
                Grades = grades
            });
        }

        return Task.FromResult(result);
    }
}