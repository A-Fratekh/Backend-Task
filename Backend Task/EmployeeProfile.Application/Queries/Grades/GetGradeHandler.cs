using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetGradeHandler : IRequestHandler<GetGradeQuery, GradeDTO>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;

    public GetGradeHandler(IQueryRepository<Grade> gradeQueryRepository, IQueryRepository<Occupation> occupationQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }

    public async Task<GradeDTO> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepository.GetByIdAsync(request.Id);
        var occupation = await _occupationQueryRepository.GetByIdAsync(grade.OccupationId);

        return new GradeDTO
        {
            Id = grade.Id,
            Name = grade.Name,
            OccupationId = grade.OccupationId,
            Occupation = new OccupationDTO
            {
                Id = occupation.Id,
                Name = occupation.Name
            }
        };
    }
}