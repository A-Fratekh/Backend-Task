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

public class DeleteOccupationHandler : IRequestHandler<DeleteOccupationcommand, Guid>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOccupationHandler(IQueryRepository<Occupation> gradeQueryRepository,
        IQueryRepository<Department> departmentQueryRepository,
        ICommandRepository<Occupation> gradeCommandRepository,
        ICommandRepository<Department> departmentCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationQueryRepository = gradeQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _occupationCommandRepository = gradeCommandRepository;
        _departmentCommandRepository = departmentCommandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(DeleteOccupationcommand request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);
        if (occupation == null) 
            throw new ArgumentNullException($"Occupation with ID : {request.Id} couldn't be found");

        await _occupationCommandRepository.DeleteAsync(occupation);
            var departments = await _departmentQueryRepository.GetAllAsync(null);
            foreach (var dept in departments)
            {
                dept.OccupationIds.Remove(occupation.Id);
                dept.Update(dept.Name, dept.OccupationIds);
            await _departmentCommandRepository.UpdateAsync(dept);
            }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
