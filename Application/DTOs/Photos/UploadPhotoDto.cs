public class UploadPhotoDto
{
    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public IFormFile File { get; set; } = null!;
}