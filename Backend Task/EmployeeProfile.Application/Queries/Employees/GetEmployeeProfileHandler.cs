using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeeProfileHandler : IRequestHandler<GetEmployeeProfileQuery, Employee>
{
    private readonly IQueryRepository<Employee> _employeeRepository;
    private readonly IQueryRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Occupation> _occupationRepository;
    private readonly IQueryRepository<Grade> _gradeRepository;

    public GetEmployeeProfileHandler(IQueryRepository<Employee> employeeRepository,
        IQueryRepository<Department> departmentRepository,
        IQueryRepository<Occupation> occupationRepository,
        IQueryRepository<Grade> gradeRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _occupationRepository = occupationRepository;
        _gradeRepository = gradeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeProfileQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee == null)
            return null;

        var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);
        var occupation = await _occupationRepository.GetByIdAsync(employee.OccupationId);
        var grade = await _gradeRepository.GetByIdAsync(employee.GradeId);

        return new Employee
        (
            employee.EmployeeNo,
            employee.Name,
            employee.HireDate,
            department.Id,
            occupation.Id,
            grade.Id
        );
    }
}
