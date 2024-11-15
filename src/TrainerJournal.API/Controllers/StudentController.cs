using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("students")]
[Authorize]
public class StudentController(
    IGroupService groupService,
    IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentItemDto>>> GetStudents([FromQuery] bool withGroup = true)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var errorOr = await studentService.GetStudentsByTrainerAsync(Guid.Parse(trainerId), withGroup);
        return this.ToActionResult(errorOr, Ok);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<CreateStudentResponse>> CreateGroupStudentAsync(CreateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await studentService.CreateAsync(request);
        return this.ToActionResult(errorOr, value => CreatedAtAction("CreateGroupStudent", value));
    }

    [HttpGet("{username}/groups")]
    public async Task<ActionResult<List<GroupDto>>> GetStudentGroupsAsync(string username)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var errorOr = await groupService.GetGroupsByStudentUsernameAsync(username);
        return this.ToActionResult(errorOr, Ok);
    }
}