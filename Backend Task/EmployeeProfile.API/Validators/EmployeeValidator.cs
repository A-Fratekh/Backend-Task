using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Employees;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;
public class EmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public EmployeeValidator() {

        RuleFor(e => e.Name).NotEmpty().WithMessage("Employee name is required")
     .Length(3, 50).WithMessage("Employee name must be between 3 and 50 characters");
        RuleFor(x => x.HireDate)
        .NotEmpty().WithMessage("Hire date is required.")
        .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Hire date cannot be in the future.");
    }

}
