using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TrainerJournal.API.Extensions;
using TrainerJournal.Infrastructure.Data;

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuth(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddInfrastructureLayer();

var app = builder.Build();

await using var serviceScope = app.Services.CreateAsyncScope();
await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "123");

app.MapControllers();

app.Run();