using MediatR;
using PhotoStore.Application.Features.Photos.Models;

namespace PhotoStore.Application.Features.Photos.Queries.GetPhotoById;
public record GetPhotoByIdQuery(Guid Id)
    :IRequest<PhotoDto?>;