
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Domain.Repositories;
public interface ICommandRepository <T> where T : AggregateRoot
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
