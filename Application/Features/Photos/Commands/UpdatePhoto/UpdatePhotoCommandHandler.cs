using MediatR;
using PhotoStore.Application.Errors;
using PhotoStore.Application.Features.Photos.Models;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Enums;
using PhotoStore.Exceptions;

namespace PhotoStore.Application.Features.Photos.Commands.UpdatePhoto;
public class UpdatePhotoCommandHandler
    :IRequestHandler<UpdatePhotoCommand, PhotoDto>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IFileService _fileService;
    public UpdatePhotoCommandHandler(IPhotoRepository photoRepository, IFileService fileService)
    {
        _photoRepository = photoRepository;
        _fileService = fileService;
    }
    public async Task<PhotoDto> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetByIdAsync(request.Id, cancellationToken);
         
        if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);
        
        if (photo.Status == PhotoStatus.Archived)
            throw new BusinessException(PhotoErrors.Archived);

        if(request.File != null)
        {
            if(!string.IsNullOrWhiteSpace(photo.FileName))
            { 
                await _fileService.DeleteAsync(photo.FileName!, cancellationToken);
            }

            var newFileName =
                await _fileService.SaveAsync(request.File, cancellationToken);

            photo.FileName = newFileName;
            photo.FilePath = $"/images/{newFileName}";
        }

        photo.Title = request.Title;
        photo.Price = request.Price;

        await _photoRepository.UpdateAsync(photo,cancellationToken);

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