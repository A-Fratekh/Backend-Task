using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Application.UnitOfWork;
using EmployeeProfile.Domain.Aggregates;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;

namespace EmployeeProfile.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public int SaveChanges()
    {
        return  _context.SaveChanges();
    }
}

