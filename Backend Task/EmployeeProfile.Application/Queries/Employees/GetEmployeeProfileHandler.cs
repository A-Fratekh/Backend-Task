using EmployeeProfile.Application.DTOs;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Queries.Employees;

public class GetEmployeeProfileHandler : IRequestHandler<GetEmployeeProfileQuery, EmployeeProfileDTO>
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

    public Task<EmployeeProfileDTO> Handle(GetEmployeeProfileQuery request, CancellationToken cancellationToken)
    {
        var employee = _employeeRepository.GetById(request.EmployeeNo);
        if (employee == null)
            return null;

        var department = _departmentRepository.GetById(employee.DepartmentId);
        var occupation = _occupationRepository.GetById(employee.OccupationId);
        var grade = _gradeRepository.GetById(employee.GradeId);

        return Task.FromResult(new EmployeeProfileDTO
        {
            
            EmployeeNo = employee.EmployeeNo,
            Name = employee.Name,
            HireDate=employee.HireDate,
            Department=department.Name,
            Occupation = occupation.Name,
            Grade = grade.Name
        }
        );
    }
}
