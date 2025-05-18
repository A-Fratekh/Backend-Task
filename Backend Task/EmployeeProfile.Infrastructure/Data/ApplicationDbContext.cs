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
    public DbSet<OccupationGrade> OccupationGrades { get; set; }
    public DbSet<DepartmentOccupation> DepartmentOccupations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OccupationGrade>(
            entity => {
                entity.HasKey(e => new { e.OccupationId, e.GradeId });
                entity.HasOne<Occupation>()
                .WithMany(o=>o.OccupationGrades)
                .HasForeignKey(og => og.OccupationId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Grade>()
                .WithMany()
                .HasForeignKey(og => og.GradeId)
                .OnDelete(DeleteBehavior.Cascade);
                
                }
            
            );

        modelBuilder.Entity<DepartmentOccupation>(
            entity => {
                entity.HasKey(e => new { e.OccupationId, e.DepartmentId });
                entity.HasOne<Department>()
                .WithMany(d => d.DepartmentOccupations)
                .HasForeignKey(od => od.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Occupation>()
                .WithMany()
                .HasForeignKey(od => od.OccupationId)
                .OnDelete(DeleteBehavior.Cascade);


            }

            );

        // Department
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Occupation
        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
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