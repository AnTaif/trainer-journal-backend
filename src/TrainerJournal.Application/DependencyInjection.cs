using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Auth;
using TrainerJournal.Application.Auth.Token;

namespace TrainerJournal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}