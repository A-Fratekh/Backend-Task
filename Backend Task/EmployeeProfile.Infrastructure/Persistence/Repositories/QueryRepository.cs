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

namespace EmployeeProfile.Application.Persistence.Repositories;

public class QueryRepository<T> : IQueryRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public QueryRepository(AppDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public IEnumerable<T> GetAll(string? orderBy)
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

        return query.ToList();
    }

    public T GetById(Guid id)
    {
        var entity = _context.Set<T>().Find(id);

        if (entity == null)
            throw new KeyNotFoundException($"Entity with ID {id} not found");

        return entity;
    }

    public T GetById(int id)
    {
        var entity = _context.Set<T>().Find(id);

        if (entity == null)
            throw new KeyNotFoundException($"Entity with Number {id} not found");

        return entity;
    }
}