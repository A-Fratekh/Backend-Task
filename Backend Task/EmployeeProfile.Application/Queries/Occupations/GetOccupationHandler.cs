using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Application.Queries.Occcupations;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Occupations;

public class GetOccupationHandler : IRequestHandler<GetOccupationQuery, OccupationDTO>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    public GetOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
    }
    public async Task<OccupationDTO> Handle(GetOccupationQuery request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);

        return new OccupationDTO {
            Id = occupation.Id,
            Name = occupation.Name,
        
        };
    }
}
