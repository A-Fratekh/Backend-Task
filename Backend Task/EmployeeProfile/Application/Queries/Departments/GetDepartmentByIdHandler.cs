using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Departments
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDTO>
    {
        private readonly IQueryRepository<DepartmentDTO> _departmentRepository;

        public GetDepartmentByIdHandler(IQueryRepository<DepartmentDTO> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
                return null;

            return new DepartmentDTO
            {
                Name = department.Name
            };
        }
    }
}
