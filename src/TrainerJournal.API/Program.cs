using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TrainerJournal.API.Extensions;
using TrainerJournal.API.Middlewares;
using TrainerJournal.Application;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure;
using TrainerJournal.Infrastructure.Data;

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Trainer Journal API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddJwtAuth(builder.Configuration.GetSection("JwtOptions"));

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer();

var app = builder.Build();

app.UseMiddleware<StatusExceptionsHandlingMiddleware>();

await using (var serviceScope = app.Services.CreateAsyncScope()) 
{
    using (var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>())
    await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    {
        await dbContext.Database.MigrateAsync();
        await DataSeeder.SeedUsersAsync(userManager);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "lalilulelolalilulelo");

app.MapControllers();

app.Run();