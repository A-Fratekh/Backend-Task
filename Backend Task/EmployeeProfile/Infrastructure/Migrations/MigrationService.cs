using EmployeeProfile.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace EmployeeProfile.Infrastructure.Migrations
{
    public class MigrationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MigrationService> _logger;

        public MigrationService(
            IServiceProvider serviceProvider,
            ILogger<MigrationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                _logger.LogInformation("Applying migrations...");
                await dbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation("Migrations applied successfully.");

                // Seed initial data if needed
                await SeedDataAsync(dbContext, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying migrations.");
                throw;
            }
        }

        private async Task SeedDataAsync(AppDbContext dbContext, CancellationToken cancellationToken)
        {
            if (!await dbContext.Departments.AnyAsync(cancellationToken))
            {
                _logger.LogInformation("Seeding database...");

                var hrDepartment = new Domain.Aggregates.DepartmentAggregate.Department("Human Resources");
                var itDepartment = new Domain.Aggregates.DepartmentAggregate.Department("Information Technology");

                await dbContext.Departments.AddRangeAsync(new[] { hrDepartment, itDepartment }, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Database seeded successfully.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}