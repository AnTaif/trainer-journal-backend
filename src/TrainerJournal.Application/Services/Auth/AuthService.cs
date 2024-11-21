using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Auth.Dtos.Requests;
using TrainerJournal.Application.Services.Auth.Dtos.Responses;
using TrainerJournal.Application.Services.Auth.Tokens;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Auth;

public class AuthService(
    UserManager<User> userManager,
    ITokenGenerator tokenGenerator,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            logger.LogWarning("User does not exists: {username}", request.Username);
            return Error.NotFound("User not found");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            logger.LogWarning("Invalid password for user: {username}", request.Username);
            return Error.BadRequest("Bad credentials");
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenGenerator.GenerateToken(user, roles);

        return new LoginResponse
        {
            UserName = user.UserName!,
            Token = token
        };
    }
}