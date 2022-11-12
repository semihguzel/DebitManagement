using DebitManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitManagement.Repository.ModelConfigurations;

public class DebitConfigurations : IEntityTypeConfiguration<Debit>
{
    public void Configure(EntityTypeBuilder<Debit> builder)
    {
        builder.ToTable("Debit");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnType("timestamp");
        builder.Property(x => x.ReturnDate).HasColumnType("timestamp");
        builder.Property(x => x.Status).HasMaxLength(50);

        builder.HasOne(x => x.User).WithMany(x => x.Debits).HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Product).WithMany(x => x.Debits).HasForeignKey(x => x.ProductId);
    }
}