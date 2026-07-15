using MediatR;

namespace PhotoStore.Application.Features.Photos.Commands.ArchivePhoto;
public record ArchivePhotoCommand
(
    Guid Id
):IRequest;