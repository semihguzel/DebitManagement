using DebitManagement.Data.Entities;
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
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new ProductConfigurations());
        modelBuilder.ApplyConfiguration(new ProductTypeConfigurations());
    }

    public DbSet<User?> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
}