
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Domain.Repositories;

public interface IQueryRepository<T> where T : AggregateRoot
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(string? orderBy);
    Task<bool> ExistsAsync(Guid id); 
}
