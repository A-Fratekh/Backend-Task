using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetGradeHandler : IRequestHandler<GetGradeQuery, GradeDTO>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    public GetGradeHandler(
        IQueryRepository<Grade> gradeQueryRepository,
        IQueryRepository<Occupation> occupationQueryRepository
        )
    {
        _gradeQueryRepository = gradeQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }


    public Task<GradeDTO> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = _gradeQueryRepository.GetById(request.Id);
        var occupations = new List<string>();
        foreach (var occupationId in grade.OccupationIds) { 
            var occupation= _occupationQueryRepository.GetById(occupationId);
            occupations.Add(occupation.Name);
        }
        return Task.FromResult(new GradeDTO
        {
            Id = grade.Id,
            Name = grade.Name,
            OccupationIds=grade.OccupationIds,
            Occupations = occupations
            
        });
    }
}