using Ahsan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
            .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Employee)
            .WithMany()
            .HasForeignKey(ce => ce.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);  //When Company Employee is deleted it's Id is disposed of as well 

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(ce => ce.CompanyId)
            .OnDelete(DeleteBehavior.SetNull); //When COmpany Employee is deleted it's Company shouldn't be! 

        modelBuilder.Entity<CompanyEmployee>()
            .HasOne(ce => ce.Position)
            .WithMany()
            .HasForeignKey(ce => ce.PositionId)
            .OnDelete(DeleteBehavior.Cascade); //When CompanyEmployee is deleted, it's position also gone with him/her.

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Company)
            .WithMany(c => c.Issues)
            .HasForeignKey(i => i.CompanyId)
            .OnDelete(DeleteBehavior.SetNull); //When Issue is deleted, it shouldn't have any effects to the company

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); //When Issue is deleted, it's category is gone alongside.

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.AssignedUser)
            .WithMany(au => au.Assignments)
            .HasForeignKey(i => i.AssignedId)
            .OnDelete(DeleteBehavior.SetNull); //When Issue is disposed of it's assigned user shouldn't be deleted from the source

        modelBuilder.Entity<IssueCategory>()
            .HasOne(ic => ic.Company)
            .WithMany(c => c.IssueCategories)
            .HasForeignKey(ic => ic.CompanyId)
            .OnDelete(DeleteBehavior.SetNull); // When Issue Category is deleted Company shouldn't be deleted.

        modelBuilder.Entity<User>()
             .HasOne<UserImage>()
             .WithOne(ui => ui.User)
             .HasForeignKey<UserImage>(u => u.UserId)
             .OnDelete(DeleteBehavior.Cascade); //when the User is gone, User's image is gone with him


    }
    #endregion
}
