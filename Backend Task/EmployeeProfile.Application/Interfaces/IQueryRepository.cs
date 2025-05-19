
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Domain.Repositories;

public interface IQueryRepository<T> where T : class
{
    T GetById(Guid id);
    T GetById(int id);
    IEnumerable<T> GetAll(string? orderBy);
}
