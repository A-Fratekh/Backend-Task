using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;

namespace EmployeeProfile.Infrastructure.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);
             _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
             _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}