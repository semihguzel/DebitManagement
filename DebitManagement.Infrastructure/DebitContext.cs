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
    }

    public DbSet<User?> Users { get; set; }
}