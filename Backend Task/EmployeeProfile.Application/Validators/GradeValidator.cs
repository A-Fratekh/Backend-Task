using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.Commands.Grades;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using FluentValidation;

namespace EmployeeProfile.Application.Validators;

public class GradeValidator : AbstractValidator<CreateGradeCommand>
{
    private readonly IQueryRepository<Occupation> _occupationRepository;
    public GradeValidator(IQueryRepository<Occupation> occupationRepository) {
        _occupationRepository = occupationRepository;

        RuleFor(g=>g.Name).NotEmpty().WithMessage("Grade name cannot be empty")
            .MinimumLength(5).WithMessage("Grade name must be at least 5 characters");
        RuleFor(g => g.OccupationId).MustAsync(async (id, _) => await _occupationRepository.ExistsAsync(id))
            .WithMessage("Occupation doesn't exist!");
    
    }
}
