using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TrainerJournal.API.Extensions;
using TrainerJournal.API.Logger;
using TrainerJournal.Application;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure;
using TrainerJournal.Infrastructure.Data;

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.With(new RemovePropertiesEnricher())
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtSecurity();

var corsOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]?>();
builder.Services.AddCustomCors(corsOrigins);

builder.Services.AddJwtAuth(builder.Configuration.GetSection("JwtOptions"));

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope()) 
{
    using (var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>())
    await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
    {
        await dbContext.Database.MigrateAsync();
        await DataSeeder.SeedOnMigratingAsync(userManager, dbContext);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => TypedResults.Content(
    content: "<html><body><a href=\"./swagger\">swagger</a></body></html>", contentType: "text/html"));

app.MapControllers();

app.Run();