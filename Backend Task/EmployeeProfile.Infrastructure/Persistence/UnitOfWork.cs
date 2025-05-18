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

public class UnitOfWork<T> : IUnitOfWork<T> where T : AggregateRoot
{
    private readonly AppDbContext _context;
    private readonly IQueryRepository<T> _readRepository;
    private readonly ICommandRepository<T> _writeRepository;

    public UnitOfWork(AppDbContext context, IQueryRepository<T> readRepository, ICommandRepository<T> writeRepository)
    {
        _context = context;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public int SaveChanges()
    {
        return  _context.SaveChanges();
    }
}

