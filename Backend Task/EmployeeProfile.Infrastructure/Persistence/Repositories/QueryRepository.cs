using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using System.Linq.Expressions;
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Application.Persistence.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
    {
        private readonly AppDbContext _context;

        public QueryRepository()
        {
            _context = new AppDbContext(disableChangeTracking: true);
        }


        public async Task<IEnumerable<T>> GetAllAsync(string? orderBy)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrEmpty(orderBy))
            {
                try
                {
                    query = query.OrderBy(e => EF.Property<object>(e, orderBy));
                }
                catch
                {
                    throw new InvalidOperationException();
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with ID {id} not found");

            return entity;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) throw new KeyNotFoundException($"Entity with Number {id} not found");

            return entity;
        }
    }
}