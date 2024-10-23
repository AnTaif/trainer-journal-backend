using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Groups;
using TrainerJournal.Application.Groups.Dtos;
using TrainerJournal.Application.Groups.Dtos.Requests;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Students.Dtos;
using TrainerJournal.Application.Students.Dtos.Requests;
using TrainerJournal.Application.Students.Dtos.Responses;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/groups")]
[Authorize]
public class GroupController(IGroupService groupService, IStudentService studentService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = RoleConstants.Trainer)]
    public async Task<ActionResult<List<GroupItemDto>>> GetAllAsync()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.GetGroupsByTrainerIdAsync(Guid.Parse(userId));

        return result.MatchFirst<ActionResult<List<GroupItemDto>>>(
            onValue: value => Ok(value),
            onFirstError: err => err.Type switch
            {
                ErrorType.NotFound => NotFound(err.Description),
                _ => BadRequest(err.Description)
            });
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GroupDto>> GetByIdAsync(Guid id)
    {
        var result = await groupService.GetGroupByIdAsync(id);

        return result.MatchFirst<ActionResult<GroupDto>>(
            onValue: value => Ok(value),
            onFirstError: err => err.Type switch
            {
                ErrorType.NotFound => NotFound(err.Description),
                _ => BadRequest(err.Description)
            });
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Trainer)]
    public async Task<ActionResult<GroupDto>> CreateGroupAsync(CreateGroupRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.CreateGroup(request, Guid.Parse(userId));

        return result.MatchFirst<ActionResult<GroupDto>>(
            onValue: value => CreatedAtAction("CreateGroup", value),
            onFirstError: err => err.Type switch
            {
                _ => BadRequest(err.Description)
            });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleConstants.Trainer)]
    public async Task<ActionResult<GroupDto>> ChangeGroupAsync(ChangeGroupRequest request, Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.ChangeGroupAsync(request, id, Guid.Parse(userId));
        
        return result.MatchFirst<ActionResult<GroupDto>>(
            onValue: value => Ok(value),
            onFirstError: err => err.Type switch
            {
                ErrorType.NotFound => NotFound(err.Description),
                ErrorType.Forbidden => Forbid(err.Description),
                _ => BadRequest(err.Description)
            });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = RoleConstants.Trainer)]
    public async Task<IActionResult> DeleteGroupAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await groupService.DeleteGroupAsync(id, Guid.Parse(userId));

        return result.MatchFirst<IActionResult>(
            onValue: _ => NoContent(),
            onFirstError: err => err.Type switch
            {
                ErrorType.NotFound => NotFound(err.Description),
                ErrorType.Forbidden => Forbid(err.Description),
                _ => BadRequest(err.Description)
            });
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult<List<StudentItemDto>>> GetGroupStudentsAsync(Guid id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.GetStudentsByGroupAsync(id, Guid.Parse(userId));

        return result.MatchFirst<ActionResult<List<StudentItemDto>>>(
            onValue: value => Ok(value),
            onFirstError: err => err.Type switch
            {
                ErrorType.NotFound => NotFound(err.Description),
                ErrorType.Forbidden => Forbid(err.Description),
                _ => BadRequest(err.Description)
            });
    }
}