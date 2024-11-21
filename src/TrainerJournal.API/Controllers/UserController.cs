using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Application.Services.Users.Dtos;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<FullInfoDto>> GetInfoAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.GetMyInfoAsync(Guid.Parse(userId));
        return result.ToActionResult(this);
    }

    [HttpPut("me")]
    public async Task<ActionResult<FullInfoWithoutCredentialsDto>> UpdateAsync([FromBody] UpdateFullInfoRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.UpdateMyInfoAsync(Guid.Parse(userId), request);
        return result.ToActionResult(this);
    }

    [HttpGet("users/{username}")]
    public async Task<ActionResult<FullInfoDto>> GetInfoByUsernameAsync(string username)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.GetInfoByUsernameAsync(Guid.Parse(userId), username);
        return result.ToActionResult(this);
    }

    [HttpPut("users/{username}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<FullInfoWithoutCredentialsDto>> ChangeUserInfoAsync(string username,
        UpdateUserStudentInfoRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.UpdateStudentInfoAsync(Guid.Parse(userId), username, request);
        return result.ToActionResult(this);
    }
}