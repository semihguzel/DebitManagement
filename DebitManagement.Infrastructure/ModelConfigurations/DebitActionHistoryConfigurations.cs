using DebitManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DebitManagement.Repository.ModelConfigurations;

public class DebitActionHistoryConfigurations : IEntityTypeConfiguration<DebitActionHistory>
{
    public void Configure(EntityTypeBuilder<DebitActionHistory> builder)
    {
        builder.ToTable("DebitActionHistory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).HasColumnType("timestamp");
        builder.Property(x => x.Status).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(100).IsRequired();

        builder.HasOne(x => x.Debit).WithMany(x => x.DebitActionHistories).HasForeignKey(x => x.DebitId);
    }
}