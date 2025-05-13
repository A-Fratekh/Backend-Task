using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Employees;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;
public class EmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    private readonly IQueryRepository<Department> _deptRepo;
    private readonly IQueryRepository<Occupation> _occupationRepo;
    private readonly IQueryRepository<Grade> _gradeRepo;

    public EmployeeValidator(IQueryRepository<Department> deptRepo, IQueryRepository<Occupation> occupationRepo, IQueryRepository<Grade> gradeRepo) {
        _deptRepo = deptRepo;
        _occupationRepo = occupationRepo;
        _gradeRepo = gradeRepo;

        RuleFor(e => e.Name).NotEmpty().WithMessage("Employee name is required")
     .Length(3, 50).WithMessage("Employee name must be between 3 and 50 characters");
        RuleFor(e=>e.DepartmentId).NotEmpty().WithMessage("Department Id is required!")
            .MustAsync(async (id,_)=> await _deptRepo.ExistsAsync(id)).WithMessage("Department does not exist!");
        RuleFor(e=>e.OccupationId).NotEmpty().WithMessage("Occupation Id is required!")
            .MustAsync(async (id, _) => await _occupationRepo.ExistsAsync(id)).WithMessage("Occupation does not exist!");
        RuleFor(e => e.GradeId).NotEmpty().WithMessage("Grade Id is required!")
            .MustAsync(async (id, _) => await _gradeRepo.ExistsAsync(id)).WithMessage("Grade does not exist!");
        RuleFor(x => x.HireDate)
        .NotEmpty().WithMessage("Hire date is required.")
        .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Hire date cannot be in the future.");
    }

}
