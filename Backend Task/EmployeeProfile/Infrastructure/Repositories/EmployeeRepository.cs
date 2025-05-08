using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
public class EmployeeRepository :
    ICommandRepository<Employee>,
    IQueryRepository<Employee>
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Employee employee) => await _context.Employees.AddAsync(employee);
    public async Task DeleteAsync(Employee employee) => await Task.FromResult(_context.Employees.Remove(employee));
    public async Task UpdateAsync(Employee employee) => await   Task.FromResult(_context.Employees.Update(employee));
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<Employee?> GetByIdAsync(Guid id) =>
        await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Grade)
            .Include(e => e.Occupation)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Employee>> GetAllAsync(string? orderBy = "HireDate")
    {
        var query = _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Grade)
            .Include(e => e.Occupation);

        if (!string.IsNullOrWhiteSpace(orderBy))
            query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Employee, Occupation>)query.OrderBy(orderBy);

        return await query.ToListAsync();
    }

    public async Task<bool> IsEmployeeNoUniqueAsync(string employeeNo) =>
        !await _context.Employees.AnyAsync(e => e.EmployeeNo == employeeNo);

    public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(Guid departmentId) =>
        await _context.Employees
            .Where(e => e.DepartmentId == departmentId)
            .ToListAsync();
}
