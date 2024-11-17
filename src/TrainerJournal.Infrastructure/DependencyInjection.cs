using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Services;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Domain.Options;
using TrainerJournal.Infrastructure.Data;
using TrainerJournal.Infrastructure.Data.Repositories;
using TrainerJournal.Infrastructure.Services;

namespace TrainerJournal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, string uploadFilesPath)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<ITrainerRepository, TrainerRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddTransient<IPracticeRepository, PracticeRepository>();
        services.AddTransient<IAttendanceRepository, AttendanceRepository>();
        services.AddTransient<IPaymentReceiptRepository, PaymentReceiptRepository>();
        services.AddTransient<ISavedFileRepository, SavedFileRepository>();

        services.AddLocalStorage(uploadFilesPath);
        //services.AddS3Storage();
        
        return services;
    }

    private static IServiceCollection AddLocalStorage(this IServiceCollection services, string uploadFilesPath)
    {
        var uploadsUrl = Environment.GetEnvironmentVariable("UPLOADS_URL") 
            ?? throw new Exception("Cannot find 'UPLOADS_URL' .env variable");

        services.Configure<UploadOptions>(options =>
        {
            options.UploadsUrl = uploadsUrl;
            options.UploadFilesPath = uploadFilesPath;
        });

        services.AddSingleton<IFileStorage, LocalFileStorage>();

        return services;
    }

    private static IServiceCollection AddS3Storage(this IServiceCollection services)
    {
        
        var s3Options = new S3Options
        {
            AccessKey = Environment.GetEnvironmentVariable("STORAGE_USER")
                   ?? throw new Exception("Cannot find 'STORAGE_USER' .env variable"),
            SecretKey = Environment.GetEnvironmentVariable("STORAGE_PASSWORD")
                       ?? throw new Exception("Cannot find 'STORAGE_PASSWORD' .env variable"),
            StorageUrl = Environment.GetEnvironmentVariable("STORAGE_URL")
                         ?? throw new Exception("Cannot find 'STORAGE_URL' .env variable")
        };

        services.Configure<S3Options>(options =>
        {
            options.AccessKey = s3Options.AccessKey;
            options.SecretKey = s3Options.SecretKey;
            options.StorageUrl = s3Options.StorageUrl;
        });

        services.AddSingleton<IFileStorage, S3FileStorage>();

        return services;
    }
    
    /// <summary>
    /// Get connection string for the database, host changes depending on the running environment (docker or locally)
    /// </summary>
    /// <returns>
    /// Connection string in form of "Host={host};Port={port};Database={database};Username={user};Password={password}"
    /// </returns>
    private static string GetConnectionString()
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_CONTAINER") ?? "localhost";
        var dbPort = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
        var dbName = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
        var dbUser = Environment.GetEnvironmentVariable("DATABASE_USER")!;
        var dbPassword = Environment.GetEnvironmentVariable("DATABASE_PASSWORD")!;
            
        return $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";
    }
}