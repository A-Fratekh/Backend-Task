using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class DeleteOccupationHandler : IRequestHandler<DeleteOccupationcommand, Guid>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Grade> _gradeCommandRepository;
    private readonly ICommandRepository<Department> _departmentCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Grade> gradeQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Grade> gradeCommandRepository,
        ICommandRepository<Department> departmentCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeQueryRepository = gradeQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _gradeCommandRepository = gradeCommandRepository;
        _departmentCommandRepository = departmentCommandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteOccupationcommand request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id) ?? throw new ArgumentNullException($"Occupation with ID : {request.Id} couldn't be found");
            foreach (var deptId in occupation.DepartmentIds)
            {
            var dept = await _departmentQueryRepository.GetByIdAsync(deptId);
                dept.RemoveDepartmentOccupation(new DepartmentOccupation(deptId, occupation.Id));
                dept.OccupationIds.Remove(occupation.Id);
                dept.Update(dept.Name, dept.OccupationIds);
            await _departmentCommandRepository.UpdateAsync(dept);
            }

         
            foreach (var gradeId in occupation.GradeIds)
            {
                var grade = await _gradeQueryRepository.GetByIdAsync(gradeId);
                occupation.RemoveOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
                grade.OccupationIds.Remove(occupation.Id);
                grade.Update(grade.Name, grade.OccupationIds);
                await _gradeCommandRepository.UpdateAsync(grade);
            
            }
        await _occupationCommandRepository.DeleteAsync(occupation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
