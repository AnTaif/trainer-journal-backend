using TrainerJournal.Domain.Exceptions;

namespace TrainerJournal.API.Middlewares;

public class StatusExceptionsHandlingMiddleware(RequestDelegate next, ILogger<StatusExceptionsHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ResponseStatusException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ResponseStatusException exception)
    {
        context.Response.StatusCode = (int)exception.StatusCode;
        context.Response.ContentType = "text/plain";

        logger.LogInformation("Handle {statusCode} status code exception: {message}", 
            (int)exception.StatusCode, exception.Message);
        return context.Response.WriteAsJsonAsync(exception.Message);
    }
}