using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Services.Auth;
using TrainerJournal.Application.Services.Auth.Tokens;
using TrainerJournal.Application.Services.Colors;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Users;

namespace TrainerJournal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IAuthService, AuthService>();

        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IGroupService, GroupService>();
        
        services.AddTransient<IColorGenerator, ColorGenerator>();

        return services;
    }
}