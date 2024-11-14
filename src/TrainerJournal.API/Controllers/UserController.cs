using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<GetUserInfoResponse>> GetInfoAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.GetInfoAsync(Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPut("me")]
    public async Task<ActionResult<GetUserInfoResponse>> UpdateAsync([FromBody] UpdateUserRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.UpdateAsync(Guid.Parse(userId), request);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<GetUserInfoResponse>> GetInfoByIdAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await userService.GetInfoAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpPut("users/{id}/student-info")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GetUserInfoResponse>> ChangeUserInfoAsync(Guid id, UpdateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();
        
        var result = await userService.UpdateStudentInfoAsync(id, request);
        return this.ToActionResult(result, Ok);
    }
}