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


        public GetDepartmentByIdHandler(IQueryRepository<Department> departmentQueryRepository,
             IQueryRepository<Occupation> occupationQueryRepository)
        {
            _departmentQueryRepository = departmentQueryRepository;
            _occupationQueryRepository = occupationQueryRepository;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentQueryRepository.GetByIdAsync(request.Id);
            if (department == null)
                throw new ArgumentNullException();


            var occupations = new List<string>();
            foreach (var occupationId in department.OccupationIds)
            {
                var occupation = await _occupationQueryRepository.GetByIdAsync(occupationId);
                occupations.Add(occupation.Name);
            }
            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                OccupationIds = department.OccupationIds,
                Occupations = occupations
            };

            return departmentDto;
        }
    }
}