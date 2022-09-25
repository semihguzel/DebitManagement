using DebitManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitManagement.Repository.ModelConfigurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductCode).IsRequired();
        builder.Property(x => x.ProductDescription).HasMaxLength(250);
        builder.Property(x => x.CreatedDate).HasColumnType("date");
        builder.HasOne(x => x.ProductType).WithMany(x => x.Products).HasForeignKey(x => x.ProductTypeId);
    }
}