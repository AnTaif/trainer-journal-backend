using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("schedule/practices")]
[Authorize(Roles = Roles.Trainer)]
public class PracticeController(
    IPracticeService practiceService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PracticeDto>> CreateSingleAsync(CreateSinglePracticeRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.CreateSinglePracticeAsync(Guid.Parse(userId), request);
        return this.ToActionResult(result, value => CreatedAtAction("CreateSingle", value));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{Roles.Trainer},{Roles.User}")]
    public async Task<ActionResult<PracticeDto>> GetPracticeAsync(Guid id, DateTime practiceDate)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.GetPractice(Guid.Parse(userId), id, practiceDate);
        return this.ToActionResult(result, Ok);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<ActionResult<PracticeDto>> CancelPracticeAsync(Guid id, CancelPracticeRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.CancelPracticeAsync(Guid.Parse(userId), id, request);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<PracticeDto>> ChangeAsync(Guid id, ChangePracticeRequest request) 
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.ChangePracticeAsync(Guid.Parse(userId), id, request);
        return this.ToActionResult(result, Ok);
    }
}