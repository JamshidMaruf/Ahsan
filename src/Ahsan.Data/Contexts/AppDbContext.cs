using Ahsan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ahsan.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Company> Companyies { get; set; }
    public virtual DbSet<CompanyEmployee> CompanyEmployees { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<IssueCategory> IssueCategories { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<UserImage> UserImages { get; set; }

    #region FluentApi
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Companies)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Employee)
            .WithMany()
            .HasForeignKey(ce => ce.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(ce => ce.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Position)
            .WithMany()
            .HasForeignKey(ce => ce.PositionId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Company)
            .WithMany(c => c.Issues)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.AssignedUser)
            .WithMany(au => au.Assignments)
            .HasForeignKey(i => i.AssignedId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<IssueCategory>()
            .HasOne(ic => ic.Company)
            .WithMany(c => c.IssueCategories)
            .HasForeignKey(ic => ic.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<User>()
        .HasMany(u => u.Companies)
        .WithOne(c => c.Owner)
        .HasForeignKey(c => c.OwnerId)
        .OnDelete(DeleteBehavior.Cascade);
    }
    #endregion
}
