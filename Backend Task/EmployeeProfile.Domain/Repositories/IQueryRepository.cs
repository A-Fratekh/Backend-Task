
namespace EmployeeProfile.Domain.Repositories;

public interface IQueryRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> GetByIdAsync(string number);
    Task<IEnumerable<T>> GetAllAsync(string? orderBy);
}
