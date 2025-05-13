using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Occupations;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;

public class OccupationValidator : AbstractValidator<CreateOccupationCommand>
{
    private readonly IQueryRepository<Department> _deptRepository;
    public OccupationValidator(IQueryRepository<Department> deptRepository)
    {
        _deptRepository = deptRepository;
        RuleFor(o=>o.Name).NotEmpty().WithMessage("Occuaption name is required")
            .MinimumLength(5).WithMessage("Occupation must be at least 5 characters");
        RuleFor(o => o.DepartmentId).MustAsync(async (id, _) => await _deptRepository.ExistsAsync(id))
            .WithMessage("Department doesn't exist!");

    }
}
