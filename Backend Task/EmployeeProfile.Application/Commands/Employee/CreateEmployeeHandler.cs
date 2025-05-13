using EmployeeProfile.Domain.Repositories;
using MediatR;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Application.UnitOfWork;

namespace EmployeeProfile.Application.Commands.Employees;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly ICommandRepository<Employee> _employeeCommandRepository;
    private readonly IQueryRepository<Employee> _employeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeHandler(ICommandRepository<Employee> employeeCommandRepository, IQueryRepository<Employee> employeeQueryRepository, IUnitOfWork unitOfWork)
    {
        _employeeCommandRepository = employeeCommandRepository;
        _employeeQueryRepository = employeeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee ( request.Name, request.HireDate, request.DepartmentId, request.OccupationId, request.GradeId);
        IEnumerable<Employee> employees = await _employeeQueryRepository.GetAllAsync(null);
        try
        {

                foreach (var e in employees)
                {
                    if (e.EmployeeNo == employee.EmployeeNo)
                    {
                        throw new InvalidOperationException();
                    }
                }
                await _employeeCommandRepository.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            return employee.EmployeeNo;
        }
        catch
        {
            throw new InvalidOperationException($"Unable To Create Employee, Employee with Employee number : {employee.EmployeeNo} Already Exists");
        }
    }
}
