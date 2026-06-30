using Microsoft.EntityFrameworkCore;
using PhotoStore.Domain.Entities;

namespace PhotoStore.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Photo> Photos { get; set; }
}