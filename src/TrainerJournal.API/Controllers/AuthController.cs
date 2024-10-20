using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Auth;
using TrainerJournal.Application.Auth.Requests;
using TrainerJournal.Application.Auth.Responses;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);
        return response;
    }
}