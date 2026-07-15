using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PhotoStore.Application.Features.Photos.Models;

namespace PhotoStore.Application.Features.Photos.Commands.UpdatePhoto;
public record UpdatePhotoCommand
(
    Guid Id,
    
    string Title,

    decimal Price,

    IFormFile File

):IRequest<PhotoDto>;