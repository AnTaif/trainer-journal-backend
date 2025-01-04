using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Route("/groups")]
[Authorize]
public class GroupController(
    IStudentService studentService,
    IGroupService groupService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GetGroupsResponse>> GetAllAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.GetGroupsByTrainerIdAsync(Guid.Parse(userId));
        return result.ToActionResult(this);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var result = await groupService.GetByIdAsync(id);
        return result.ToActionResult(this);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GroupDto>> CreateGroupAsync(CreateGroupRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.CreateAsync(request, Guid.Parse(userId));
        return result.ToActionResult(this, value => CreatedAtAction("CreateGroup", value));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GroupDto>> ChangeGroupAsync(UpdateGroupInfoRequest infoRequest, Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.UpdateInfoAsync(infoRequest, id, Guid.Parse(userId));
        return result.ToActionResult(this);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroupAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.DeleteAsync(id, Guid.Parse(userId));
        return result.ToActionResult(this);
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult<List<StudentItemDto>>> GetGroupStudentsAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.GetStudentsByGroupAsync(id, Guid.Parse(userId));
        return result.ToActionResult(this);
    }

    /// <summary>
    ///     Adds existed Student to the Group
    /// </summary>
    [HttpPost("{id}/students")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult> AddStudentToGroupAsync(Guid id, AddStudentRequest request)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result =
            await studentService.AddStudentToGroupAsync(id, request, Guid.Parse(trainerId));
        return result.ToActionResult(this, _ => NoContent());
    }

    [HttpDelete("{id}/students/{username}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult> ExcludeStudentFromGroupAsync(Guid id, string username)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result =
            await studentService.ExcludeStudentFromGroupAsync(id, username, Guid.Parse(trainerId));
        return result.ToActionResult(this);
    }
}