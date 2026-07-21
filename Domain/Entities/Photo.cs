using PhotoStore.Domain.Enums;

namespace PhotoStore.Domain.Entities;

public class Photo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public PhotoStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}