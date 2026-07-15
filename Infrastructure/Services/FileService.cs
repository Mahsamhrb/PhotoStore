using PhotoStore.Application.Interfaces;

namespace PhotoStore.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var imagesPath =
            Path.Combine(_env.WebRootPath, "images");

        Directory.CreateDirectory(imagesPath);

        var fileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var fullPath =
            Path.Combine(imagesPath, fileName);

        await using var stream =
            new FileStream(fullPath, FileMode.Create);

        await file.CopyToAsync(stream,cancellationToken);

        return fileName;
    }

    public Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Task.CompletedTask;

        var fullPath =
            Path.Combine(_env.WebRootPath,
                        "images",
                        fileName);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}