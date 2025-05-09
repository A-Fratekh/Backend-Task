using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationHandler : IRequestHandler<CreateOccupationCommand, Guid>
{
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;

    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository)
    {
        _occupationCommandRepository = occupationCommandRepository;
    }
    public async Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name, request.DepartmentId);
        await _occupationCommandRepository.AddAsync(occupation);
        return occupation.Id;
    }
}
