using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.Auth;
using TrainerJournal.Application.Services.Auth.Dtos.Requests;
using TrainerJournal.Application.Services.Auth.Dtos.Responses;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        return result.ToActionResult(this);
    }
}