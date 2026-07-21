using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Persistence.Configurations;
public class OrderConfiguration
    :IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .HasPrecision(18 , 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}