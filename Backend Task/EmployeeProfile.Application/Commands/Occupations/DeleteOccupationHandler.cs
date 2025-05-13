using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using MediatR;

namespace EmployeeProfile.Application.Commands.Occupations;

public class DeleteOccupationHandler : IRequestHandler<DeleteOccupationcommand, Guid>
{
    private readonly IQueryRepository<Occupation> _occupationQueryRepository;
    private readonly ICommandRepository<Occupation> _occupationCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOccupationHandler(IQueryRepository<Occupation> gradeQueryRepository,
        ICommandRepository<Occupation> gradeCommandRepository,
        IUnitOfWork unitOfWork)
    {
        _occupationQueryRepository = gradeQueryRepository;
        _occupationCommandRepository = gradeCommandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(DeleteOccupationcommand request, CancellationToken cancellationToken)
    {
        var occupation = await _occupationQueryRepository.GetByIdAsync(request.Id);
        if (occupation == null) 
            throw new ArgumentNullException($"Occupation with ID : {request.Id} couldn't be found");
        await _occupationCommandRepository.DeleteAsync(occupation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Id;
    }
}
