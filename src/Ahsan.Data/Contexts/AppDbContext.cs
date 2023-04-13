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

    #region FluentApi
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.Companies)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Employee)
            .WithMany()
            .HasForeignKey(ce => ce.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(ce => ce.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Position)
            .WithMany()
            .HasForeignKey(ce => ce.PositionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Company)
            .WithMany(c => c.Issues)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.AssignedUser)
            .WithMany(au => au.Assignments)
            .HasForeignKey(i => i.AssignedId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<IssueCategory>()
            .HasOne(ic => ic.Company)
            .WithMany(c => c.IssueCategories)
            .HasForeignKey(ic => ic.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    #endregion
}
