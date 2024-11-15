using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos.Requests;
using TrainerJournal.Domain.Constants;
using TrainerJournal.Domain.Enums.ViewSchedule;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Authorize]
public class ScheduleController(
    IScheduleService scheduleService) : ControllerBase
{
    [HttpGet("schedule")]
    public async Task<ActionResult<List<ScheduleItemDto>>> GetSchedule(
        [FromQuery] DateTime date, [FromQuery] ViewSchedule view = ViewSchedule.Week)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await scheduleService.GetScheduleAsync(Guid.Parse(userId), date.ToUniversalTime(), view);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("groups/{id}/schedule")]
    public async Task<ActionResult<List<ScheduleItemDto>>> GetGroupSchedule(
        Guid id, [FromQuery] DateTime date, [FromQuery] ViewSchedule view = ViewSchedule.Week)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await scheduleService.GetGroupScheduleAsync(id, date.ToUniversalTime(), view);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost("groups/{id}/schedule")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<List<ScheduleItemDto>>> CreateSchedule(Guid id, CreateScheduleRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await scheduleService.CreateScheduleAsync(Guid.Parse(userId), id, request);
        return this.ToActionResult(result, value => CreatedAtAction("CreateSchedule", value));
    }
}