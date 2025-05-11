using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;

namespace EmployeeProfile.Application.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public QueryRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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

        public async Task<T> GetByNoAsync(string number)
        {
            var entity = await _context.Set<T>().FindAsync(number);
            if (entity == null) throw new KeyNotFoundException($"Entity with Number {number} not found");

            return entity;
        }
    }
}