using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Occupations;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;

public class OccupationValidator : AbstractValidator<CreateOccupationCommand>
{
    public OccupationValidator()
    {
        RuleFor(o=>o.Name).NotEmpty().WithMessage("Occuaption name is required")
            .MinimumLength(5).WithMessage("Occupation must be at least 5 characters");

    }
}
