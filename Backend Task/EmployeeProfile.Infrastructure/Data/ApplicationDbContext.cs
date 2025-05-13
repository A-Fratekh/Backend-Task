using EmployeeProfile.Domain;
using EmployeeProfile.Domain.Aggregates.DepartmentAggregate;
using EmployeeProfile.Domain.Aggregates.EmployeeAggregate;
using EmployeeProfile.Domain.Aggregates.GradeAggregate;
using EmployeeProfile.Domain.Aggregates.OccupationAggregate;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    {
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Occupation> Occupations { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Department
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.Occupations)
            .WithMany()
            .UsingEntity<DepartmentOccupation>();
        });

        // Occupation
        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasMany(e=>e.Grades).WithMany().UsingEntity<OccupationGrade>();
        });

        // Grade
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

        });

        // Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeNo);
            entity.Property(e => e.EmployeeNo).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });
    }
}