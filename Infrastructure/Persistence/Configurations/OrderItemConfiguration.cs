using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Persistence.Configurations;
public class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.PhotoTitle)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Photo)
            .WithMany()
            .HasForeignKey(x => x.PhotoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Download)
            .WithOne(x => x.OrderItem)
            .HasForeignKey<Download>(x => x.OrderItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}