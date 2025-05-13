using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetGradeHandler : IRequestHandler<GetGradeQuery, GradeDTO>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;


    public GetGradeHandler(IQueryRepository<Grade> gradeQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;

    }

    public async Task<GradeDTO> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepository.GetByIdAsync(request.Id);
        return new GradeDTO
        {
            Id = grade.Id,
            Name = grade.Name,
            
        };
    }
}