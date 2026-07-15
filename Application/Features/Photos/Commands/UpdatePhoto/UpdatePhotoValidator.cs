using FluentValidation;
using PhotoStore.Application.Features.Photos.Commands.UpdatePhoto;

public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
{
    private const int MaxFileSize = 20* 1024 * 1024; 

    public UpdatePhotoCommandValidator()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Photo Id is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        When(x => x.File != null, () =>
        {
            RuleFor(x => x.File!.Length)
                .GreaterThan(0)
                .WithMessage("File cannot be empty")
                .LessThanOrEqualTo(MaxFileSize)
                .WithMessage("File Size must be less than 20MB");

            RuleFor(x => x.File!.FileName)
                .Must(BeValidExtension)
                .WithMessage("Only jpg, jpeg, png, webp allowed");
        });
    }

    private bool BeValidExtension(string fileName)
    {
        var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return allowed.Contains(ext);
    }
}