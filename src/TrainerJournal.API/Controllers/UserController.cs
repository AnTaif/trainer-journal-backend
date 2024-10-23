using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Users;
using TrainerJournal.Application.Users.Dtos.Responses;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("me")]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetUserInfoResponse>> GetInfoAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.GetInfoAsync(Guid.Parse(userId));

        return result.MatchFirst<ActionResult<GetUserInfoResponse>>(
            onValue: response => Ok(response),
            onFirstError: error => error.Type switch
            {
                ErrorType.NotFound => NotFound(error.Description),
                _ => BadRequest(error.Description)
            });
    }
}