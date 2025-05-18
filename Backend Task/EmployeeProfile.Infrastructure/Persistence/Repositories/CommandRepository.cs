using EmployeeProfile.Domain.Aggregates;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;

namespace EmployeeProfile.Infrastructure.Persistence.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
    {
        private readonly AppDbContext _context;
        public CommandRepository(AppDbContext context)
        {

            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
           

        }
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

             _context.Set<T>().Update(entity);
            
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
            
        }
    }
}