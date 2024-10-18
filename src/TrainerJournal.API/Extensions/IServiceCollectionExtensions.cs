using Microsoft.EntityFrameworkCore;
using TrainerJournal.Infrastructure.Data;

namespace TrainerJournal.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
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