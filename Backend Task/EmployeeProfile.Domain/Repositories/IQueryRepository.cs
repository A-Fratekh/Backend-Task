
namespace EmployeeProfile.Domain.Repositories;

public interface IQueryRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(string? orderBy);
}
