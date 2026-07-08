namespace PhotoStore.Application.Interfaces;

public interface IFileService
{
    Task<string> SaveAsync(IFormFile file);

    Task DeleteAsync(string fileName);
}