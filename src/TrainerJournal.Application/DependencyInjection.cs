using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Application.Services.Auth;
using TrainerJournal.Application.Services.Auth.Tokens;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Colors;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Trainers;
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
        services.AddTransient<IScheduleService, ScheduleService>();
        services.AddTransient<IPracticeManager, PracticeManager>();
        services.AddTransient<IPracticeService, PracticeService>();
        services.AddTransient<IAttendanceService, AttendanceService>();
        services.AddTransient<IPaymentReceiptService, PaymentReceiptService>();
        services.AddTransient<IBalanceChangeManager, BalanceChangeManager>();
        services.AddTransient<IBalanceChangeService, BalanceChangeService>();
        services.AddTransient<ITrainerService, TrainerService>();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        services.AddTransient<IColorGenerator, ColorGenerator>();

        return services;
    }
}