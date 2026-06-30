namespace PhotoStore.Application.DTOs.Photos;
public class CreatePhotoDto
{
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public decimal Price { get; set; }
}