using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using EmployeeProfile.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IQueryRepository<Department>, DepartmentRepository>();
        services.AddScoped<ICommandRepository<Department>, DepartmentRepository>();
        services.AddScoped<IQueryRepository<Employee>, EmployeeRepository>();
        services.AddScoped<ICommandRepository<Employee>, EmployeeRepository>();
        services.AddScoped<IQueryRepository<Occupation>, OccupationRepository>();
        services.AddScoped<ICommandRepository<Occupation>, OccupationRepository>();
        services.AddScoped<IQueryRepository<Grade>, GradeRepository>();
        services.AddScoped<ICommandRepository<Grade>, GradeRepository>();

        services.AddHostedService<MigrationService>();

        return services;
    }
}