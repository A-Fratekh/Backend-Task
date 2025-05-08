using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
public class GradeRepository :
    ICommandRepository<Grade>,
    IQueryRepository<Grade>
{
    private readonly AppDbContext _context;

    public GradeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Grade grade) => await _context.Grades.AddAsync(grade);
    public async Task DeleteAsync(Grade grade) => await Task.FromResult(_context.Grades.Remove(grade));
    public async Task UpdateAsync(Grade grade) => await Task.FromResult(_context.Grades.Update(grade));
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Grade?> GetByIdAsync(Guid id) =>
        await _context.Grades.FindAsync(id);

    public async Task<IEnumerable<Grade>> GetAllAsync(string? orderBy ="HireDate")
    {
        var query = _context.Grades.AsQueryable();
        if (!string.IsNullOrWhiteSpace(orderBy))
            query = query.OrderBy(orderBy);
        return await query.ToListAsync();
    }


    public async Task<bool> CanDeleteAsync(Guid gradeId) =>
        !await _context.Employees.AnyAsync(e => e.GradeId == gradeId);
}
