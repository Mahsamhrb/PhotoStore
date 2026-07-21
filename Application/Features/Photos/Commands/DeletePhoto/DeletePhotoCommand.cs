using MediatR;

namespace PhotoStore.Application.Features.Photos.Commands.DeletePhoto;
public record DeletePhotoCommand
(
    
    Guid Id

) : IRequest;