using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Grades;

public class GetGradeHandler : IRequestHandler<GetGradeQuery, GradeDTO>
{
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;


    public GetGradeHandler(IQueryRepository<Grade> gradeQueryRepository, IQueryRepository<Occupation> occupationQueryRepository, IQueryRepository<Department> departmentQueryRepository)
    {
        _gradeQueryRepository = gradeQueryRepository;
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
    }

    public async Task<GradeDTO> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _gradeQueryRepository.GetByIdAsync(request.Id);
        var occupation = await _occupationQueryRepository.GetByIdAsync(grade.OccupationId);
        var dept = await _departmentQueryRepository.GetByIdAsync(occupation.DepartmentId);
        return new GradeDTO
        {
            Id = grade.Id,
            Name = grade.Name,
            Occupation = new OccupationDTO
            {
                Id = occupation.Id,
                Name = occupation.Name,
                department = new DepartmentDTO
                {
                    Id = dept.Id,
                    Name = dept.Name

                },

            },
            
        };
    }
}