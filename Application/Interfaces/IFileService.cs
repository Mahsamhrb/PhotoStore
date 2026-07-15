namespace PhotoStore.Application.Interfaces;

public interface IFileService
{
    Task<string> SaveAsync(IFormFile file,  CancellationToken cancellationToken = default);

    Task DeleteAsync(string fileName ,  CancellationToken cancellationToken = default);
}