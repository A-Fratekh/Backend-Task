
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Domain.Repositories;

public interface IQueryRepository<T> where T : AggregateRoot
{
    T GetById(Guid id);
    T GetById(int id);
    IEnumerable<T> GetAll(string? orderBy);
}
