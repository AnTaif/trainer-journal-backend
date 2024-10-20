namespace TrainerJournal.API.Middlewares;

public class UserLoggingMiddleware(RequestDelegate next, ILogger<UserLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string userId;
        if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            userId = "Anonymous";
        else
            userId = context.User.Identity.Name ?? "Unknown";

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["UserId"] = userId
               }))
        {
            await next(context);
        }
    }
}