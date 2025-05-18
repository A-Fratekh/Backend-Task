using EmployeeProfile.Domain.Aggregates;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;

namespace EmployeeProfile.Infrastructure.Persistence.Repositories;

public class CommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
{
    private readonly AppDbContext _context;
    public CommandRepository(AppDbContext context)
    {

        _context = context;
    }
    public void Add(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

         _context.Set<T>().Add(entity);
       

    }
    public void Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

         _context.Set<T>().Update(entity);
        
    }
    public void Delete(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Remove(entity);
        
    }
}