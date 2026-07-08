using FluentValidation;
using PhotoStore.Application.DTOs.Photos;

public class UploadPhotoDtoValidator : AbstractValidator<UploadPhotoDto>
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
    private const int MaxFileSize = 20 * 1024 * 1024;

    public UploadPhotoDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price Is Required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required"); 

        RuleFor(x => x.File!.Length)
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage("File size must be less than 20MB");

        RuleFor(x => x.File!.FileName)
            .Must(BeValidExtension)
            .WithMessage("Only jpg, jpeg, png, webp files are allowed"); 
    }

    private bool BeValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return _allowedExtensions.Contains(extension);
    }
}