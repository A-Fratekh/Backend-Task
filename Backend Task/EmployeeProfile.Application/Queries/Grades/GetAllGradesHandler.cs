using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetAllGradesHandler : IRequestHandler<GetAllGradesQuery, List<Grade>>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    public GetAllGradesHandler(IQueryRepository<Grade> gradeQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
    }

    public async Task<List<Grade>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
    {
        var grades = await _gradeQueryRepository.GetAllAsync(null);

        return grades.ToList();
    }
}
