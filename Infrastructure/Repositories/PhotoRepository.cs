using Microsoft.EntityFrameworkCore;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Entities;
using PhotoStore.Infrastructure.Data;

namespace PhotoStore.Infrastructure.Repositories;
public class PhotoRepository : IPhotoRepository
{
    private readonly AppDbContext _context;
    public PhotoRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Photo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Photos.ToListAsync(cancellationToken);
    }
    public async Task<Photo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Photos
            .FirstOrDefaultAsync(x => x.Id == id,
            cancellationToken);
    }
    public async Task AddAsync(Photo photo ,
    CancellationToken cancellationToken = default)
    {
        await _context.Photos.AddAsync(photo, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

    }
    public async Task UpdateAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        _context.Photos.Update(photo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}