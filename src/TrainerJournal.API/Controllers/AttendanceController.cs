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

    [HttpGet("students/{username}/attendance")]
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

        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost("students/{username}/attendance/mark")]
    public async Task<ActionResult<AttendanceMarkDto?>> MarkAttendanceAsync(string username, MarkAttendanceRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.MarkAttendanceAsync(Guid.Parse(userId), username, request);

        return this.ToActionResult(result, Ok);
    }
    
    [HttpDelete("students/{username}/attendance/mark")]
    public async Task<ActionResult<AttendanceMarkDto?>> UnmarkAttendanceAsync(string username, MarkAttendanceRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await attendanceService.MarkAttendanceAsync(Guid.Parse(userId), username, request);

        return this.ToActionResult(result, Ok);
    }
}