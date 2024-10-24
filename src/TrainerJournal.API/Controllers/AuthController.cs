using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Services.Auth;
using TrainerJournal.Application.Services.Auth.Dtos.Requests;
using TrainerJournal.Application.Services.Auth.Dtos.Responses;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        return result.MatchFirst<ActionResult<LoginResponse>>(
            onValue: response => Ok(response), 
            onFirstError: error => error.Type switch
        {
            ErrorType.NotFound => NotFound(error.Description),
            _ => BadRequest(error.Description)
        });
    }
}