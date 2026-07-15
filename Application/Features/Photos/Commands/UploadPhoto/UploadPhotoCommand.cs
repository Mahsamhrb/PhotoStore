using MediatR;
using PhotoStore.Application.Features.Photos.Models;

namespace PhotoStore.Application.Features.Photos.Commands.UploadPhoto;
public record UploadPhotoCommand
(
    string Title,

    decimal Price,

    IFormFile File

): IRequest<PhotoDto>;