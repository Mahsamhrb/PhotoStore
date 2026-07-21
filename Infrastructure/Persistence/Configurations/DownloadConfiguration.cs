using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Persistence.Configurations;
public class DownloadConfiguration
    : IEntityTypeConfiguration<Download>
{
    public void Configure(EntityTypeBuilder<Download> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DownloadCount)
            .IsRequired();

        builder.Property(x => x.MaxDownloads)
            .IsRequired()
            .HasDefaultValue(3);

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.Property(x => x.LastDownloadedAt)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.OrderItem)
            .WithOne(x => x.Download)
            .HasForeignKey<Download>(x => x.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}