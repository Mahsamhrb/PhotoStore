using PhotoStore.Domain.Enums;

namespace PhotoStore.Application.Features.Photos.Models;

public class PhotoDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? FileName { get; set; }

    public decimal Price { get; set; }

    public string? FilePath { get; set; }

    public PhotoStatus Status { get; set; }
}