using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class CreateOccupationHandler : IRequestHandler<CreateOccupationCommand, Guid>
{
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOccupationHandler(ICommandRepository<Occupation> occupationCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationCommandRepository = occupationCommandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(CreateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = new Occupation(request.Name, request.DepartmentId);
        await _occupationCommandRepository.AddAsync(occupation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return occupation.Id;
    }
}
