using Microsoft.EntityFrameworkCore;
using PhotoStore.Application.DTOs.Photos;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Entities;
using PhotoStore.Infrastructure.Data;

namespace PhotoStore.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<PhotoService> _logger;

    public PhotoService(AppDbContext context , IWebHostEnvironment env, ILogger<PhotoService> logger)
    {
        _context = context;
        _env = env;
        _logger = logger;
    }
    public async Task<List<PhotoDto>> GetAll()
    {
        return await _context.Photos
            .AsNoTracking()
            .Select(p => new PhotoDto
            {
                Id = p.Id,
                Title = p.Title,
                FileName = p.FileName,
                Price = p.Price,
                FilePath = p.FilePath
            })
            .ToListAsync();
    }
    public async Task<PhotoDto?> GetById(int id)
    {
        return await _context.Photos
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PhotoDto
            {
                Id = p.Id,
                Title = p.Title,
                FileName = p.FileName,
                Price = p.Price,
                FilePath = p.FilePath
            })
            .FirstOrDefaultAsync();
    }
    public async Task<PhotoDto> Upload(UploadPhotoDto dto)
    {
        var imagesPath = Path.Combine(_env.WebRootPath, "images");
        Directory.CreateDirectory(imagesPath);

        if (dto.File is null || dto.File.Length == 0)
            throw new ArgumentException("File is required.");

        var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
        var fullPath = Path.Combine(imagesPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var photo = new Photo
        {
            Title = dto.Title,
            Price = dto.Price,
            FileName = fileName,
            FilePath = $"/images/{fileName}"
        };

        await _context.Photos.AddAsync(photo);
        await _context.SaveChangesAsync();

        return new PhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            FileName = photo.FileName,
            Price = photo.Price,
            FilePath = photo.FilePath
        };
    }
    public async Task<bool> Delete(int id)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            return false;

        try
        {
            if (!string.IsNullOrWhiteSpace(photo.FileName))
            {
                var fullPath = Path.Combine(_env.WebRootPath, "images", photo.FileName);

                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"Failed to delete file for PhotoId {PhotoId}", id);            
        }
        
        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<PhotoDto?> Update(int id, UpdatePhotoDto dto)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            return null;
        
        if (dto.File != null)
        {
            if(!string.IsNullOrWhiteSpace(photo.FileName))
            {
                var oldPath = Path.Combine(_env.WebRootPath , "images" , photo.FileName);

                if (File.Exists(oldPath))
                    File.Delete(oldPath);
            }

            var newFileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
            var newPath = Path.Combine(_env.WebRootPath , "images", newFileName);

            using (var stream = new FileStream(newPath , FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            photo.FileName = newFileName;
            photo.FilePath =$"/images/{newFileName}";
        }

        photo.Title = dto.Title;
        photo.Price = dto.Price;

        await _context.SaveChangesAsync();

        return new PhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            FileName = photo.FileName,
            Price = photo.Price,
            FilePath = photo.FilePath
        };
    }
}