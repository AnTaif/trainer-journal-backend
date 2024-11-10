using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Authorize]
public class AttendanceController(IAttendanceService attendanceService) : ControllerBase
{
    [HttpGet("groups/{id}/attendance")]
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

        return this.ToActionResult(result, Ok);
    }

    [HttpGet("students/{id}/attendance")]
    public async Task<ActionResult<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(
        Guid id, 
        [FromQuery] DateTime start,
        [FromQuery] DateTime? end = null)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.GetStudentAttendanceAsync(
            Guid.Parse(userId), 
            id, 
            start, 
            end ?? start + TimeSpan.FromDays(30));

        return this.ToActionResult(result, Ok);
    }

    [HttpPost("students/{id}/attendance/mark-unmark")]
    public async Task<ActionResult<AttendanceMarkDto?>> MarkAttendanceAsync(Guid id, AttendanceMarkRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.MarkUnmarkAttendanceAsync(Guid.Parse(userId), id, request);

        return this.ToActionResult(result, Ok);
    }
}