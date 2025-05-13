using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Occupations;

public class GetAllOccupationsHandler : IRequestHandler<GetAllOccupationsQuery, List<OccupationDTO>>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;

    public GetAllOccupationsHandler(IQueryRepository<Occupation> occupationQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
    }

    public async Task<List<OccupationDTO>> Handle(GetAllOccupationsQuery request, CancellationToken cancellationToken)
    {
        var occupations = await _occupationQueryRepository.GetAllAsync(null);
        var result = new List<OccupationDTO>();
        foreach(var occupation in occupations)
        {

            result.Add(new OccupationDTO
            {
                Id = occupation.Id,
                Name = occupation.Name,
                
            });
        }

        return result;
    }
}