using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Persistence.Configurations;
public class PhotoConfiguration
    : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}