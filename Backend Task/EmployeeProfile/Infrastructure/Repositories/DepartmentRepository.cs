using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

public class DepartmentRepository :
    ICommandRepository<Department>,
    IQueryRepository<Department>
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Department department) => await _context.Departments.AddAsync(department);
    public async Task DeleteAsync(Department department) => await Task.FromResult(_context.Departments.Remove(department));
    public async Task UpdateAsync(Department department) => await Task.FromResult(_context.Departments.Update(department));
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Department?> GetByIdAsync(Guid id) =>
        await _context.Departments.FindAsync(id);

    public async Task<IEnumerable<Department>> GetAllAsync(string? orderBy = null)
    {
        var query = _context.Departments.AsQueryable();
        if (!string.IsNullOrWhiteSpace(orderBy))
            query = query.OrderBy(orderBy);
        return await query.ToListAsync();
    }


    public async Task<IEnumerable<Department>> GetByNameAsync(string name) =>
        await _context.Departments
            .Where(d => d.Name.Contains(name))
            .ToListAsync();

    public async Task<bool> IsNameUniqueAsync(string name) =>
        !await _context.Departments.AnyAsync(d => d.Name.ToLower() == name.ToLower());

    public async Task<bool> CanDeleteAsync(Guid id) =>
        !await _context.Employees.AnyAsync(e => e.DepartmentId == id);
}
