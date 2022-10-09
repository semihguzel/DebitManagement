using System.Reflection;
using DebitManagement.Core.Entities;
using DebitManagement.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DebitManagement.Repository;

public class DebitContext : DbContext
{
    public DebitContext(DbContextOptions<DebitContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User?> Users { get; set; }
    public DbSet<UserRole?> UserRoles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType?> ProductTypes { get; set; }
    public DbSet<Debit> Debits { get; set; }
}