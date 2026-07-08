using System.Net;
using System.Text.Json;
using FluentValidation;
using PhotoStore.Common;
using PhotoStore.Exceptions;

namespace PhotoStore.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception occurred");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ApiResponse
        {
            Success = false
        };

        switch (exception)
        {
            case FluentValidation.ValidationException validationException:

                context.Response.StatusCode = 
                    StatusCodes.Status400BadRequest;

                response.Message = "Validation failed.";

                response.Errors = validationException.Errors
                    .Select(failure => new ValidationError
                    {
                        Property = failure.PropertyName,
                        Message = failure.ErrorMessage
                    })
                    .ToList();

                break;

            case BusinessException:

                context.Response.StatusCode = 
                    StatusCodes.Status400BadRequest;

                response.Message = exception.Message;

                break;

            case NotFoundException:

                context.Response.StatusCode =
                    StatusCodes.Status404NotFound;

                response.Message = exception.Message;

                break;

            default:
                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                response.Message =
                    "An unexpected error occurred.";

                break;
        }

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}