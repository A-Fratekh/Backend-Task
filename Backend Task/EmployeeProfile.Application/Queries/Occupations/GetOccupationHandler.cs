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
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    public GetOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository, IQueryRepository<Department> departmentQueryRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
    }
    public async Task<OccupationDTO> Handle(GetOccupationQuery request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);
        var department = await _departmentQueryRepository.GetByIdAsync(occupation.DepartmentId);

        return new OccupationDTO {
            Id = occupation.Id,
            Name = occupation.Name,
            department = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            }
        
        };
    }
}
