using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeeProfile.Domain.Repositories;
using EmployeeProfile.Infrastructure.Data;
using EmployeeProfile.Infrastructure.Migrations;
using EmployeeProfile.Infrastructure.Persistence.Repositories;


namespace EmployeeProfile.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                }
                options.UseSqlServer(connectionString,
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddHostedService<MigrationService>();



            return services;
        }
    }
}