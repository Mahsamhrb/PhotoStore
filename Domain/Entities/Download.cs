namespace PhotoStore.Domain.Entities;

public class Download
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrderItemId { get; set; }

    public int DownloadCount { get; set; }

    public int MaxDownloads { get; set; } = 3;

    public DateTime ExpiresAt { get; set; }

    public DateTime? LastDownloadedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public OrderItem OrderItem { get; set; } = null!;
}