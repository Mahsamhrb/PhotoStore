using MediatR;
using PhotoStore.Application.Features.Photos.Models;

namespace PhotoStore.Application.Features.Photos.Queries.GetAllPhotos;
public record GetAllPhotosQuery
    :IRequest<List<PhotoDto>>;