using MediatR;
using PhotoStore.Application.Errors;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Enums;
using PhotoStore.Exceptions;

namespace PhotoStore.Application.Features.Photos.Commands.ArchivePhoto;
public class ArchivePhotoCommandHandler
    :IRequestHandler<ArchivePhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    public ArchivePhotoCommandHandler(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }
    public async Task Handle(ArchivePhotoCommand request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetByIdAsync(request.Id, cancellationToken);

         if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);


        if (photo.Status == PhotoStatus.Archived)
            throw new BusinessException(PhotoErrors.Archived);

        photo.Status = PhotoStatus.Archived;

        await _photoRepository.UpdateAsync(photo,cancellationToken);
    }
}