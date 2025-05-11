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
           

            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Occupations = occupations.Select(o => new OccupationDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    department=new DepartmentDTO
                    {
                        Id = o.DepartmentId,
                        Name = o.Department.Name,
                    }
                 
                }).ToList()
            };

            return departmentDto;
        }
    }
}