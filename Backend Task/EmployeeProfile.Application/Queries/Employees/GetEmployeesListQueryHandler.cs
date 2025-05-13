using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, List<EmployeeProfileDTO>>
{

    private readonly IQueryRepository<Employee> _employeeRepository;
    private readonly IQueryRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationRepository;
    private readonly IQueryRepository<Grade> _gradeRepository;

    public GetEmployeesListQueryHandler(IQueryRepository<Employee> employeeRepository,
        IQueryRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationRepository,
        IQueryRepository<Grade> gradeRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _occupationRepository = occupationRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<List<EmployeeProfileDTO>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetAllAsync(request.OrderBy);
        var result = new List<EmployeeProfileDTO>();

        foreach (var employee in employees)
        {
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
            var occupation = await _occupationRepository.GetByIdAsync(employee.OccupationId);
            var grade = await _gradeRepository.GetByIdAsync(employee.GradeId);

            result.Add(new EmployeeProfileDTO
            {
                EmployeeNo = employee.EmployeeNo,
                Name = employee.Name,
                Department = department.Name,
                Occupation = occupation?.Name,
                Grade = grade?.Name,
                HireDate = employee.HireDate
            });
        }

        return result;
    }
}
