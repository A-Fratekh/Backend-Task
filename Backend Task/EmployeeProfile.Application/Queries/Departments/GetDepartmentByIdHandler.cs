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
        private readonly IQueryRepository<DepartmentOccupation> _deptOccQueryRepository;



        public GetDepartmentByIdHandler(IQueryRepository<Department> departmentQueryRepository,
             IQueryRepository<Occupation> occupationQueryRepository,
             IQueryRepository<DepartmentOccupation> deptOccQueryRepository)
        {
            _departmentQueryRepository = departmentQueryRepository;
            _occupationQueryRepository = occupationQueryRepository;
            _deptOccQueryRepository = deptOccQueryRepository;
        }

        public Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = _departmentQueryRepository.GetById(request.Id);
            if (department == null)
                throw new ArgumentNullException();
            var deptOccs = _deptOccQueryRepository.GetAll(null);
            var occupations = new List<string>();
            foreach (var deptOcc in deptOccs)
            {
                if (deptOcc.DepartmentId == department.Id)
                {
                    var occupation = _occupationQueryRepository.GetById(deptOcc.OccupationId);
                    occupations.Add(occupation.Name);
                }
            }
            var departmentDto = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Occupations = occupations
            };

            return Task.FromResult(departmentDto);
        }
    }
}