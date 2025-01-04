using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.BalanceChanges.Dtos;
using TrainerJournal.Application.Services.BalanceChanges.Dtos.Responses;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Route("students")]
[Authorize]
public class StudentController(
    IBalanceChangeService balanceChangeService,
    IGroupService groupService,
    IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentItemDto>>> GetStudents([FromQuery] bool withGroup = true)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = await studentService.GetStudentsByTrainerAsync(Guid.Parse(trainerId), withGroup);
        return result.ToActionResult(this);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<CreateStudentResponse>> CreateGroupStudentAsync(CreateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.CreateAsync(request);
        return result.ToActionResult(this,
            value => CreatedAtAction("CreateGroupStudent", value));
    }

    [HttpGet("{username}/groups")]
    public async Task<ActionResult<List<GroupDto>>> GetStudentGroupsAsync(string username)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.GetGroupsByStudentUsernameAsync(username);
        return result.ToActionResult(this);
    }
    
    [HttpGet("{username}/balance-changes")]
    public async Task<ActionResult<List<GetStudentBalanceChangeResponse>>> GetStudentBalanceChangesAsync(
        string username, DateTime start, DateTime end)
    {
        var result = await balanceChangeService.GetStudentBalanceChanges(username, start, end);
        return result.ToActionResult(this);
    }

    [HttpGet("{username}/balance-changes/report")]
    public async Task<ActionResult<BalanceChangeReportDto>> GetStudentBalanceChangeReportAsync(
        string username, DateTime start, DateTime end)
    {
        var result = await balanceChangeService.GetStudentBalanceChangeReportAsync(username, start, end);
        return result.ToActionResult(this);
    }
}