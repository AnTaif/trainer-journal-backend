using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Trainers;
using TrainerJournal.Infrastructure.Data;
using TrainerJournal.Infrastructure.Data.Repositories;

namespace TrainerJournal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<ITrainerRepository, TrainerRepository>();
        
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