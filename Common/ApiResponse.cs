namespace PhotoStore.Common;

public class ApiResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public IReadOnlyList<ValidationError>? Errors { get; set; }
}