using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeProfile.Infrastructure.Migrations
{
    public class MigrationService : IHostedService
    {
        private readonly ILogger<MigrationService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(
            ILogger<MigrationService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting database migration");

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    if (dbContext.Database.IsSqlServer())
                    {
                        _logger.LogInformation("Ensuring database exists and applying migrations");
                        await dbContext.Database.MigrateAsync(cancellationToken);
                        _logger.LogInformation("Database migration completed successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating the database");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}