using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/groups")]
[Authorize]
public class GroupController(
    IGroupService groupService, 
    IStudentService studentService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<List<GroupItemDto>>> GetAllAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.GetGroupsByTrainerIdAsync(Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GroupDto>> CreateGroupAsync(CreateGroupRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.CreateGroup(request, Guid.Parse(userId));
        return this.ToActionResult(result, value => CreatedAtAction("CreateGroup", value));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var result = await groupService.GetGroupByIdAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<GroupDto>> ChangeGroupAsync(ChangeGroupRequest request, Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.ChangeGroupAsync(request, id, Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<IActionResult> DeleteGroupAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.DeleteGroupAsync(id, Guid.Parse(userId));
        return this.ToActionResult(result, _ => NoContent());
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult<List<StudentItemDto>>> GetGroupStudentsAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.GetStudentsByGroupAsync(id, Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }

    [HttpPost("{id}/students")]
    public async Task<ActionResult<CreateStudentResponse>> CreateGroupStudentAsync(Guid id,
        CreateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.CreateAsync(request, id);
        return this.ToActionResult(result, value => CreatedAtAction("CreateGroupStudent", value));
    }
}