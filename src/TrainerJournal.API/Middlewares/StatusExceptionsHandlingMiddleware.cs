using TrainerJournal.Domain.Exceptions;

namespace TrainerJournal.API.Middlewares;

public class StatusExceptionsHandlingMiddleware(RequestDelegate next)
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

        return context.Response.WriteAsJsonAsync(exception.Message);
    }
}