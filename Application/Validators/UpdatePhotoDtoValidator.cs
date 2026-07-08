using FluentValidation;
using PhotoStore.Application.DTOs.Photos;

public class UpdatePhotoDtoValidator : AbstractValidator<UpdatePhotoDto>
{
    private const int MaxFileSize = 20* 1024 * 1024; 

    public UpdatePhotoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        When(x => x.File != null, () =>
        {
            RuleFor(x => x.File!.Length)
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
        var ext = Path.GetExtension(fileName).ToLower();
        return allowed.Contains(ext);
    }
}