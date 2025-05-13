using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;
using System;
using System.Linq;

namespace EmployeeProfile.Application.Queries.Departments
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDTO>
    {
        private readonly IQueryRepository<Department> _departmentQueryRepository;
        private readonly IQueryRepository<Occupation> _occupationQueryRepository;

        public GetDepartmentByIdHandler(IQueryRepository<Department> departmentQueryRepository, IQueryRepository<Occupation> occupationQueryRepository)
        {
            _departmentQueryRepository = departmentQueryRepository;
            _occupationQueryRepository = occupationQueryRepository;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
            if (department == null)
                return null;

            IEnumerable<Occupation> occupations = await _occupationQueryRepository.GetAllAsync(null);
            var result = new List<OccupationDTO>();

            foreach (var occupation in occupations)
            {
                if (occupation.DepartmentId == department.Id)
                { 
                    result.Add(
                        new OccupationDTO
                        {
                            
                            Id=occupation.Id,
                            Name=occupation.Name
                        }
                        );
                }
            }
            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Occupations = result
            };

            return departmentDto;
        }
    }
}