using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration
    : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);


        builder.Property(x => x.Status)
            .IsRequired();


        builder.Property(x => x.TransactionId)
            .HasMaxLength(200);


        builder.Property(x => x.Gateway)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(x => x.CreatedAt)
            .IsRequired();


        builder.HasOne(x => x.Order)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}