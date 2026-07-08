namespace PhotoStore.Common;

public class ValidationError
{
    public string Property { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;
}