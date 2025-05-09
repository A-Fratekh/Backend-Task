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
        var occupationDTOs = occupations.Select(o => new OccupationDTO
        {
            Id = o.Id,
            Name = o.Name,
            DeptId = o.DepartmentId,
            department = o.Department != null ? new DepartmentDTO
            {
                Id = o.Department.Id,
                Name = o.Department.Name
            } : null
        }).ToList();

        return occupationDTOs;
    }
}