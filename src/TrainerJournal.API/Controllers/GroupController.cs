using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/groups")]
[Authorize(Roles = Roles.Trainer)]
public class GroupController(
    IStudentService studentService,
    IGroupService groupService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetGroupsResponse>> GetAllAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.GetGroupsByTrainerIdAsync(Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var result = await groupService.GetByIdAsync(id);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost]
    public async Task<ActionResult<GroupDto>> CreateGroupAsync(CreateGroupRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.CreateAsync(request, Guid.Parse(userId));
        return this.ToActionResult(result, value => CreatedAtAction("CreateGroup", value));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GroupDto>> ChangeGroupAsync(UpdateGroupInfoRequest infoRequest, Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.UpdateInfoAsync(infoRequest, id, Guid.Parse(userId));
        return this.ToActionResult(result, Ok);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroupAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.DeleteAsync(id, Guid.Parse(userId));
        return this.ToActionResult(result, _ => NoContent());
    }
    
    [HttpGet("{id}/students")]
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
    [HttpPost("{id}/students")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<StudentInfoDto>> AddStudentToGroupAsync(Guid id, AddStudentRequest request)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = 
            await studentService.AddStudentToGroupAsync(id, request, Guid.Parse(trainerId));
        return this.ToActionResult(result, Ok);
    }
    
    [HttpDelete("{groupId}/students/{studentId}")]
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