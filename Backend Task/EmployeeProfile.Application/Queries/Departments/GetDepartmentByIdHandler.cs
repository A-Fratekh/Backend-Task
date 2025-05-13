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

        public GetDepartmentByIdHandler(IQueryRepository<Department> departmentQueryRepository)
        {
            _departmentQueryRepository = departmentQueryRepository;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
            if (department == null)
                throw new ArgumentNullException();


            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
            };

            return departmentDto;
        }
    }
}