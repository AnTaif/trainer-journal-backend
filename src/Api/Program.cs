using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Api.Logger;
using TrainerJournal.Application;
using TrainerJournal.Application.Services.Users;
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

var contentRootPath = builder.Environment.ContentRootPath;
var uploadFilesPath = Environment.GetEnvironmentVariable("FILES_PATH") ?? Path.Combine(contentRootPath, "Uploads");

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(uploadFilesPath);

var app = builder.Build();

await using (var serviceScope = app.Services.CreateAsyncScope()) 
{
    using (var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>())
    {
        var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
        await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>())
        {
            await dbContext.Database.MigrateAsync();
            await DataSeeder.SeedOnMigratingAsync(userManager, userService, dbContext);
        }
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

Directory.CreateDirectory(Path.Combine(uploadFilesPath, "Public"));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(uploadFilesPath, "Public")),
    RequestPath = "/uploads"
});

app.MapGet("/", () => TypedResults.Content(
    content: "<html><body><a href=\"./swagger\">swagger</a></body></html>", contentType: "text/html"));

app.MapControllers();

app.Run();