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
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/groups")]
[Authorize(Roles = Roles.Trainer)]
public class GroupController(
    IGroupService groupService, 
    IStudentService studentService) : ControllerBase
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

    [HttpPost("{id}/price")]
    public async Task<ActionResult<GroupDto>> SetGroupPriceAsync(Guid id, float newPrice)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.SetPriceAsync(Guid.Parse(userId), id, newPrice);
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