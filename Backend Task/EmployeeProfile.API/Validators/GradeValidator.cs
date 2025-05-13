using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Grades;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;

public class GradeValidator : AbstractValidator<CreateGradeCommand>
{
    public GradeValidator() {
        RuleFor(g=>g.Name).NotEmpty().WithMessage("Grade name cannot be empty")
            .MinimumLength(5).WithMessage("Grade name must be at least 5 characters");
    
    }
}
