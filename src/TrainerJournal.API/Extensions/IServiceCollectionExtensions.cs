using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
}