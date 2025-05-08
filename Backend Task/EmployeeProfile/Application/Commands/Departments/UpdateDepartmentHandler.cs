
using MediatR;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Application.Commands.Departments;

namespace EmployeeProfile.Application.Commands.Departmentsl;

public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Guid>
{
    private readonly DepartmentRepository _departmentRepository;

    public UpdateDepartmentHandler(DepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department =  await _departmentRepository.GetByIdAsync(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");

        department.Update(request.Name);
        await _departmentRepository.UpdateAsync(department);
        return request.Id;
    }
}
