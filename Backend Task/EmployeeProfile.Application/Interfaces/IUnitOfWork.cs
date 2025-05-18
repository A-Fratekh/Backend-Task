using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeProfile.Domain.Aggregates;

namespace EmployeeProfile.Application.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
}
