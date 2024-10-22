using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Auth.Dtos.Requests;
using TrainerJournal.Application.Auth.Dtos.Responses;
using TrainerJournal.Application.Auth.Token;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Exceptions;

namespace TrainerJournal.Application.Auth;

public class AuthService(
    UserManager<User> userManager, 
    ITokenGenerator tokenGenerator,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        logger.LogInformation("Trying to login user: {userName}", request.UserName);
        var user = await userManager.FindByNameAsync(request.UserName);
        if (user == null)
            throw new NotFoundException("User not found");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
            throw new BadRequestException("Bad credentials");
        
        var roles = await userManager.GetRolesAsync(user);

        var token = tokenGenerator.GenerateToken(user, roles);

        logger.LogInformation("User '{userName}' logged in successfully", request.UserName);
        return new LoginResponse(user.Id, user.UserName!, token);
    }
}