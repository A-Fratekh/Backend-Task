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

public class DeleteOccupationHandler : IRequestHandler<DeleteOccupationcommand>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Grade> _gradeCommandRepository;
    private readonly ICommandRepository<Department> _departmentCommandRepository;

    public DeleteOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository,
        IQueryRepository<Department> departmentQueryRepository,
        IQueryRepository<Grade> gradeQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Grade> gradeCommandRepository,
        ICommandRepository<Department> departmentCommandRepository)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeQueryRepository = gradeQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _gradeCommandRepository = gradeCommandRepository;
        _departmentCommandRepository = departmentCommandRepository;
    }

    public Task Handle(DeleteOccupationcommand request, CancellationToken cancellationToken)
    {
        var occupation =  _occupationQueryRepository.GetById(request.Id) ?? throw new ArgumentNullException($"Occupation with ID : {request.Id} couldn't be found");
        foreach (var deptId in occupation.DepartmentIds)
        {
            var dept = _departmentQueryRepository.GetById(deptId);
            dept.RemoveDepartmentOccupation(new DepartmentOccupation(deptId, occupation.Id));
            dept.OccupationIds.Remove(occupation.Id);
            dept.Update(dept.Name, dept.OccupationIds);
            _departmentCommandRepository.Update(dept);
        }
        foreach (var gradeId in occupation.GradeIds)
        {
            var grade = _gradeQueryRepository.GetById(gradeId);
            occupation.RemoveOccupationGrade(new OccupationGrade(occupation.Id, gradeId));
            grade.OccupationIds.Remove(occupation.Id);
            grade.Update(grade.Name, grade.OccupationIds);
                _gradeCommandRepository.Update(grade);
            
        }
         _occupationCommandRepository.Delete(occupation);
        return Task.CompletedTask;
    }
}
