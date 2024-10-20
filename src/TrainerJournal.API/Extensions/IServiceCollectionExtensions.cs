using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrainerJournal.Application.Auth;
using TrainerJournal.Application.Auth.Token;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Options;
using TrainerJournal.Infrastructure.Data;

namespace TrainerJournal.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddJwtAuth(this IServiceCollection services, IConfigurationSection jwtSection)
    {
        var jwtOptions = new JwtOptions();
        jwtSection.Bind(jwtOptions);

        jwtOptions.Secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? 
                            throw new ArgumentNullException(nameof(services), "JWT_SECRET environment variable is not set");

        services.Configure<JwtOptions>(options =>
        {
            options.Audience = jwtOptions.Audience;
            options.Issuer = jwtOptions.Issuer;
            options.Secret = jwtOptions.Secret;
            options.ExpiryMinutes = jwtOptions.ExpiryMinutes;
        });

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                //options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization();
    }

    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IAuthService, AuthService>();
    }
    
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