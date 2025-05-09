using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class UpdateOccupationHandler : IRequestHandler<UpdateOccupationCommand, Guid>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;

    public UpdateOccupationHandler(IQueryRepository<Occupation> gradeQueryRepository, ICommandRepository<Occupation> gradeCommandRepository)
    {
        _occupationQueryRepository = gradeQueryRepository;
        _occupationCommandRepository = gradeCommandRepository;
    }

    public async Task<Guid> Handle(UpdateOccupationCommand request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);
        if (occupation == null) throw new ArgumentNullException($"Occupation with Id : {request.Id} couldn't be found");

        occupation.Update(request.Name);
        await _occupationCommandRepository.UpdateAsync(occupation);
        return request.Id;
    }
}
