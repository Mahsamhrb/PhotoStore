using MediatR;
using PhotoStore.Application.Interfaces;
using PhotoStore.Application.Features.Photos.Models;

namespace PhotoStore.Application.Features.Photos.Queries.GetAllPhotos;
public class GetAllPhotosQueryHandler
    : IRequestHandler<GetAllPhotosQuery, List<PhotoDto>>
{
    private readonly IPhotoRepository _photoRepository;
    public GetAllPhotosQueryHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }
    public async Task<List<PhotoDto>> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
    {
        var photos = await _photoRepository.GetAllAsync(cancellationToken);

        return photos.Select(photo => new PhotoDto
        {
            Id = photo.Id,
            Title = photo.Title,
            FileName = photo.FileName,
            Price = photo.Price,
            FilePath = photo.FilePath,
            Status = photo.Status
        }).ToList();
    }
}