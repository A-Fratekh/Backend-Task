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

namespace EmployeeProfile.Application.Queries.Grades;

public class GetAllGradesHandler : IRequestHandler<GetAllGradesQuery, List<GradeDTO>>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    public GetAllGradesHandler(IQueryRepository<Grade> gradeQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
    }

    public async Task<List<GradeDTO>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
    {
        var result = new List<GradeDTO>();
        var grades = await _gradeQueryRepository.GetAllAsync(null);

        foreach(var grade in grades)
        {

            result.Add(
                new GradeDTO
                {
                    Id = grade.Id,
                    Name = grade.Name,
                    Occupations = grade.Occupations

                });
        }

        return result;
    }
}
