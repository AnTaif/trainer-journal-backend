using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Authorize]
public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
{
    [HttpGet("attendance/groups/{id}")]
    public async Task<ActionResult<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(
        Guid id,
        [FromQuery] DateTime start,
        [FromQuery] DateTime? end = null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.GetGroupAttendanceAsync(
            Guid.Parse(userId),
            id,
            start,
            end ?? start + TimeSpan.FromDays(30));

        return result.ToActionResult(this);
    }

    [HttpGet("attendance/students/{username}")]
    public async Task<ActionResult<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(
        string username,
        [FromQuery] DateTime start,
        [FromQuery] DateTime? end = null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.GetStudentAttendanceAsync(
            Guid.Parse(userId),
            username,
            start,
            end ?? start + TimeSpan.FromDays(30));

        return result.ToActionResult(this);
    }

    [HttpPost("attendance/students/{username}/mark")]
    public async Task<ActionResult<AttendanceMarkDto?>> MarkAttendanceAsync(string username,
        MarkAttendanceRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.MarkAttendanceAsync(Guid.Parse(userId), username, request);

        return result.ToActionResult(this);
    }

    [HttpDelete("attendance/students/{username}/mark")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<AttendanceMarkDto?>> UnmarkAttendanceAsync(string username,
        MarkAttendanceRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.UnmarkAttendanceAsync(Guid.Parse(userId), username, request);

        return result.ToActionResult(this);
    }

    [HttpGet("attendance/practices/{id}")]
    public async Task<ActionResult<List<GetPracticeAttendanceResponse>>> GetPracticeAttendanceAsync(Guid id,
        DateTime practiceStart)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.GetPracticeAttendanceAsync(Guid.Parse(userId), id, practiceStart);
        return result.ToActionResult(this);
    }

    [HttpPost("attendance/practices/{id}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult> MarkPracticeAttendanceAsync(Guid id, MarkPracticeAttendanceRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.MarkPracticeAttendanceAsync(Guid.Parse(userId), id, request);
        return result.ToActionResult(this, value => 
            CreatedAtAction("MarkPracticeAttendance", value));
    }
}