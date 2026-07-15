using PhotoStore.Domain.Enums;

namespace PhotoStore.Domain.Entities;

public class Photo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public decimal Price { get; set; }
    public string? FilePath { get; set; }
    public PhotoStatus Status { get; set; }
    public int PurchaseCount { get; set; }   
}