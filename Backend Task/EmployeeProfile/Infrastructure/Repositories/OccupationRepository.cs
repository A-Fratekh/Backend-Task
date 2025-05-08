using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
public class OccupationRepository :
    ICommandRepository<Occupation>,
    IQueryRepository<Occupation>
{
    private readonly AppDbContext _context;

    public OccupationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Occupation o) => await _context.Occupations.AddAsync(o);
    public async Task DeleteAsync(Occupation o) => await Task.FromResult(_context.Occupations.Remove(o));
    public async Task UpdateAsync(Occupation o) => await Task.FromResult(_context.Occupations.Update(o));
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Occupation?> GetByIdAsync(Guid id) =>
        await _context.Occupations.FindAsync(id);

    public async Task<IEnumerable<Occupation>> GetAllAsync(string? orderBy = null)
    {
        var query = _context.Occupations.AsQueryable();
        if (!string.IsNullOrWhiteSpace(orderBy))
            query = query.OrderBy(orderBy);
        return await query.ToListAsync();
    }

    public async Task<bool> IsNameUniqueAsync(string name) =>
        !await _context.Occupations.AnyAsync(o => o.Name.ToLower() == name.ToLower());

    public async Task<bool> CanDeleteAsync(Guid occupationId) =>
        !await _context.Employees.AnyAsync(e => e.OccupationId == occupationId);
}
