using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Authorize]
public class StudentController(
    IStudentService studentService) : ControllerBase
{
    [HttpGet("students")]
    public async Task<ActionResult<List<StudentItemDto>>> GetStudents([FromQuery] bool withGroup = true)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = await studentService.GetStudentsByTrainerAsync(Guid.Parse(trainerId), withGroup);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost("students")]
    public async Task<ActionResult<CreateStudentResponse>> CreateGroupStudentAsync(CreateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.CreateAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction("CreateGroupStudent", value));
    }
    
    [HttpGet("groups/{id}/students")]
    [Authorize]
    public async Task<ActionResult<List<StudentItemDto>>> GetGroupStudentsAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.GetStudentsByGroupAsync(id, Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }
    
    /// <summary>
    /// Adds existed Student to the Group
    /// </summary>
    [HttpPost("groups/{id}/students")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<StudentInfoDto>> AddStudentToGroupAsync(Guid id, AddStudentRequest request)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = 
            await studentService.AddStudentToGroupAsync(id, request, Guid.Parse(trainerId));
        return this.ToActionResult(result, Ok);
    }
    
    [HttpDelete("groups/{groupId}/students/{studentId}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<StudentInfoDto>> ExcludeStudentFromGroupAsync(Guid groupId, Guid studentId)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = 
            await studentService.ExcludeStudentFromGroupAsync(groupId, studentId, Guid.Parse(trainerId));
        return this.ToActionResult(result, Ok);
    }
}