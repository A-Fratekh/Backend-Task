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
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<OccupationGrade> _occGradeQueryRepository;


    public GetAllGradesHandler(IQueryRepository<Grade> gradeQueryRepository,
        IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<OccupationGrade> occGradeQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _occGradeQueryRepository = occGradeQueryRepository;
    }

    public Task<List<GradeDTO>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
    {
        var result = new List<GradeDTO>();
        var grades =  _gradeQueryRepository.GetAll(null);
        var occGrades = _occGradeQueryRepository.GetAll(null);

        foreach (var grade in grades)
        {
            var occupations = new List<string>();
            foreach (var occGrade in occGrades)
            {
                if (occGrade.GradeId == grade.Id)
                {
                    var occupation = _occupationQueryRepository.GetById(occGrade.OccupationId);
                    occupations.Add(occupation.Name);
                }
            }

            result.Add(
                new GradeDTO
                {
                    Id = grade.Id,
                    Name = grade.Name,
                    Occupations = occupations

                });
        }

        return Task.FromResult(result);
    }
}
