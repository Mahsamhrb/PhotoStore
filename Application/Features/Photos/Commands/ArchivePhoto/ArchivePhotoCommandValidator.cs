using FluentValidation;

namespace PhotoStore.Application.Features.Photos.Commands.ArchivePhoto;
public class ArchivePhotoCommandValidator
     : AbstractValidator<ArchivePhotoCommand>
{
     public ArchivePhotoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Photo Id is required");
    }
}