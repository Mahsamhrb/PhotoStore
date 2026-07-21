using PhotoStore.Domain.Entities;

namespace PhotoStore.Application.Interfaces;
public interface IPhotoRepository
{
    Task<List<Photo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Photo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Photo photo, CancellationToken cancellationToken = default);
    Task UpdateAsync(Photo photo, CancellationToken cancellationToken = default);
    Task DeleteAsync (Photo photo , CancellationToken cancellationToken = default);
}