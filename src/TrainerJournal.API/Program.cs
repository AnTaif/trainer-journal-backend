using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sprache;
using TrainerJournal.API.Extensions;
using TrainerJournal.Domain.Options;

var a = new IdentityRole<Guid>("123") { Id = Guid.NewGuid() };

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuth(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddInfrastructureLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();