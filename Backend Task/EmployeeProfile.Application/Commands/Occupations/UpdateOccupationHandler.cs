using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class UpdateOccupationHandler : IRequestHandler<UpdateOccupationCommand, Guid>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);
        if (occupation == null) throw new ArgumentNullException($"Occupation with Id : {request.Id} couldn't be found");

        occupation.Update(request.Name, request.DepartmentIds);
        await _occupationCommandRepository.UpdateAsync(occupation);
        var departments = await _departmentQueryRepository.GetAllAsync(null);
        foreach (var departmentId in occupation.DepartmentIds)
        {
            var department = await _departmentQueryRepository.GetByIdAsync(departmentId);
            department.OccupationIds.Add(occupation.Id);
            department.Update(department.Name, department.OccupationIds);
            await _departmentRepository.UpdateAsync(department);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
