
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Domain.Repositories;
public interface ICommandRepository <T> where T : AggregateRoot
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
