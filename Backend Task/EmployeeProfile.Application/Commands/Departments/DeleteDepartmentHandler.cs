using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Departments;

public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationRepository;
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;

    public DeleteDepartmentHandler(ICommandRepository<Department> departmentRepository, IQueryRepository<Department> departmentQueryRepository, ICommandRepository<Occupation> occupationRepository,
        IQueryRepository<Occupation> occupationQueryRepository)
    {
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationRepository = occupationRepository;
        _occupationQueryRepository = occupationQueryRepository;
    }

    public  Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _departmentQueryRepository.GetById(request.Id);
        if (department == null)
            throw new Exception($"Department with id {request.Id} not found");
        var occupations = _occupationQueryRepository.GetAll(null);
        foreach (var occupation in occupations)
        {
            department.RemoveDepartmentOccupation(new DepartmentOccupation(department.Id, occupation.Id));
            occupation.DepartmentIds.Remove(department.Id);
            occupation.Update(occupation.Name, occupation.DepartmentIds, occupation.GradeIds);
            _occupationRepository.Update(occupation);
        }
        _departmentRepository.Delete(department);

        return Task.CompletedTask;
    }
}
