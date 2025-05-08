
namespace EmployeeProfile.Domain.Repositories;
public interface ICommandRepository <T> where T : class
{
    Task AddAsync(T t);
    Task UpdateAsync(T t);
    Task DeleteAsync(T t);
    Task SaveChangesAsync();
}
