using Ahsan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ahsan.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Company> Companyies { get; set; }
    public virtual DbSet<CompanyEmployee> CompanyEmployees { get; set;}
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<IssueCategory> IssueCategories { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
}
