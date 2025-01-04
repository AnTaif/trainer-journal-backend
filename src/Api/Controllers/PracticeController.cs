using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Route("schedule/practices")]
[Authorize]
public class PracticeController(
    IPracticeService practiceService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PracticeDto>> CreateSingleAsync(CreateSinglePracticeRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.CreateSinglePracticeAsync(Guid.Parse(userId), request);
        return result.ToActionResult(this,
            value => CreatedAtAction("CreateSingle", value));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PracticeDto>> GetPracticeAsync(Guid id, DateTime practiceDate)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.GetPractice(Guid.Parse(userId), id, practiceDate);
        return result.ToActionResult(this);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<PracticeDto>> CancelPracticeAsync(Guid id, CancelPracticeRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.CancelPracticeAsync(Guid.Parse(userId), id, request);
        return result.ToActionResult(this);
    }

    [HttpPost("{id}/resume")]
    public async Task<ActionResult<PracticeDto>> ResumePracticeAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.ResumePracticeAsync(Guid.Parse(userId), id);
        return result.ToActionResult(this);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PracticeDto>> ChangeAsync(Guid id, ChangePracticeRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await practiceService.ChangePracticeAsync(Guid.Parse(userId), id, request);
        return result.ToActionResult(this);
    }
}