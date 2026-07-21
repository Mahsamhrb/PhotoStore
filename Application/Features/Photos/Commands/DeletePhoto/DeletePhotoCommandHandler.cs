using MediatR;
using PhotoStore.Application.Errors;
using PhotoStore.Application.Interfaces;
using PhotoStore.Domain.Enums;
using PhotoStore.Exceptions;

namespace PhotoStore.Application.Features.Photos.Commands.DeletePhoto;
public class DeletePhotoCommandHandler
    : IRequestHandler<DeletePhotoCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IFileService _fileService;
    public DeletePhotoCommandHandler(IPhotoRepository photoRepository, IFileService fileService)
    {
        _photoRepository = photoRepository;
        _fileService = fileService;
    }
    public async Task Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (photo == null)
            throw new NotFoundException(PhotoErrors.NotFound);

        if (!string.IsNullOrWhiteSpace(photo.FileName))
        {
            await _fileService.DeleteAsync(photo.FileName, cancellationToken);
        }

        await _photoRepository.DeleteAsync(photo, cancellationToken);
    }
}