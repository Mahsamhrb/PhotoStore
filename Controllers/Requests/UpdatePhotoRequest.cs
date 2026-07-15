namespace PhotoStore.Controllers.Requests;

public record UpdatePhotoRequest(
    string Title,
    decimal Price,
    IFormFile File
);