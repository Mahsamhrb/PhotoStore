using MediatR;
using PhotoStore.Application.Features.Photos.Models;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Entities;
using PhotoStore.Domain.Enums;

namespace PhotoStore.Application.Features.Photos.Commands.UploadPhoto;
public class UploadPhotoCommandHandler
    :IRequestHandler<UploadPhotoCommand, PhotoDto>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IFileService _fileService;
    public UploadPhotoCommandHandler(IPhotoRepository photoRepository, IFileService fileService)
    {
        _photoRepository = photoRepository;
        _fileService = fileService;
    }
    public async Task<PhotoDto> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var fileName = await _fileService.SaveAsync(request.File, cancellationToken);

         var photo = new Photo
        {
            Title = request.Title,
            Price = request.Price,
            FileName = fileName,
            FilePath = $"/images/{fileName}",
            Status = PhotoStatus.Available,
            PurchaseCount = 0
        };

        await _photoRepository.AddAsync(photo,cancellationToken);
        
        return new PhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            Price = photo.Price,
            FileName = photo.FileName,
            FilePath = photo.FilePath,
            Status = photo.Status
        };
    }
}