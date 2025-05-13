using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Departments;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;

public class DepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    
    public DepartmentValidator() { 

        RuleFor(d=>d.Name).NotEmpty().WithMessage("Department name cannot be empty!")
            .MinimumLength(5).WithMessage("Department name must be at least 5 characters");
    
    }
}
