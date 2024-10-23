using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Auth;
using TrainerJournal.Application.Auth.Token;
using TrainerJournal.Application.Groups;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Users;

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

        return services;
    }
}