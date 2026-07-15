using MediatR;
using PhotoStore.Application.Errors;
using PhotoStore.Application.Features.Photos.Models;
using PhotoStore.Application.Interfaces;
using PhotoStore.Exceptions;

namespace PhotoStore.Application.Features.Photos.Queries.GetPhotoById;
public class GetPhotoByIdQueryHandler
    :IRequestHandler<GetPhotoByIdQuery, PhotoDto?>
{
    private readonly IPhotoRepository _photoRepository;
    public GetPhotoByIdQueryHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }
    public async Task<PhotoDto?> Handle(GetPhotoByIdQuery request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetByIdAsync(request.Id,cancellationToken);

        if (photo == null)
        {
            throw new NotFoundException(PhotoErrors.NotFound);
        }

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