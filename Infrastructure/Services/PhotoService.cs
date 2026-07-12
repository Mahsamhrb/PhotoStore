using Microsoft.EntityFrameworkCore;
using PhotoStore.Application.DTOs.Photos;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Entities;
using PhotoStore.Infrastructure.Data;
using PhotoStore.Domain.Enums;
using PhotoStore.Application.Errors;
using PhotoStore.Exceptions;

namespace PhotoStore.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public PhotoService(AppDbContext context , IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
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
                FilePath = p.FilePath,
                Status = p.Status
            })
            .ToListAsync();
    }
   public async Task<PhotoDto> GetById(int id)
    {
        var photo = await _context.Photos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);


        return MapToDto(photo);
    }

    public async Task<PhotoDto> Upload(UploadPhotoDto dto)
    {
        var fileName = await _fileService.SaveAsync(dto.File!);

        var photo = new Photo
        {
            Title = dto.Title,
            Price = dto.Price,
            FileName = fileName,
            FilePath = $"/images/{fileName}",
            Status = PhotoStatus.Available,
            PurchaseCount = 0
        };

        await _context.Photos.AddAsync(photo);

        await _context.SaveChangesAsync();

        return MapToDto(photo);
    }

    public async Task Archive(int id)
    {
        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);


        if (photo.Status == PhotoStatus.Archived)
            throw new BusinessException(PhotoErrors.Archived);


        photo.Status = PhotoStatus.Archived;

        await _context.SaveChangesAsync();
    }
    public async Task<PhotoDto> Update(int id, UpdatePhotoDto dto)
    {
        var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);
        
        if (photo.Status == PhotoStatus.Archived)
            throw new BusinessException(PhotoErrors.Archived);

        if (dto.File != null)
        {
            if (!string.IsNullOrWhiteSpace(photo.FileName))
            {
                await _fileService.DeleteAsync(photo.FileName!);
            }

            var newFileName =
                await _fileService.SaveAsync(dto.File);

            photo.FileName = newFileName;
            photo.FilePath = $"/images/{newFileName}";
        }

        photo.Title = dto.Title;
        photo.Price = dto.Price;

        await _context.SaveChangesAsync();

        return MapToDto(photo);
    }

    private static PhotoDto MapToDto(Photo photo)
    {
        return new PhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            FileName = photo.FileName,
            Price = photo.Price,
            FilePath = photo.FilePath,
            Status = photo.Status
        };
    }
}