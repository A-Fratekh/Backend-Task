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

public class UpdateOccupationHandler : IRequestHandler<UpdateOccupationCommand>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly ICommandRepository<Department> _departmentRepository;
    private readonly IQueryRepository<Department> _departmentQueryRepository;
    private readonly ICommandRepository<Grade> _gradeRepository;
    private readonly IQueryRepository<Grade> _gradeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOccupationHandler(IQueryRepository<Occupation> occupationQueryRepository,
        ICommandRepository<Occupation> occupationCommandRepository,
        ICommandRepository<Department> departmentRepository,
        IQueryRepository<Department> departmentQueryRepository,
        ICommandRepository<Grade> gradeRepository,
        IQueryRepository<Grade> gradeQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationQueryRepository = occupationQueryRepository;
        _occupationCommandRepository = occupationCommandRepository;
        _departmentRepository = departmentRepository;
        _departmentQueryRepository = departmentQueryRepository;
        _gradeRepository = gradeRepository;
        _gradeQueryRepository = gradeQueryRepository;
        _unitOfWork = unitOfWork;
    }

    public  Task Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = _occupationQueryRepository.GetById(request.Id);
        occupation = occupation?? throw new ArgumentNullException($"Occupation with Id : {request.Id} couldn't be found");
        occupation.Update(request.Name, request.DepartmentIds, request.GradeIds);

         _occupationCommandRepository.Update(occupation);

        foreach (var departmentId in occupation.DepartmentIds)
        {
            var department = _departmentQueryRepository.GetById(departmentId);
            if (!department.OccupationIds.Contains(occupation.Id))
            {
                department.OccupationIds.Add(occupation.Id);
                department.Update(department.Name, department.OccupationIds);
                department.AddDepartmentOccupation(new DepartmentOccupation(departmentId, occupation.Id));
                _departmentRepository.Update(department);
            }
        }

        foreach (var gradeId in occupation.GradeIds)
        {
            var grade = _gradeQueryRepository.GetById(gradeId);
            if (!grade.OccupationIds.Contains(occupation.Id))
            {
                grade.OccupationIds.Add(occupation.Id);
                grade.Update(grade.Name, grade.OccupationIds);
                _gradeRepository.Update(grade);
            }
        }

        return Task.CompletedTask;
    }
}
