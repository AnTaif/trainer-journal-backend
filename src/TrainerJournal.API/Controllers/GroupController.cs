using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Students.Dtos.Requests;
using TrainerJournal.Application.Students.Dtos.Responses;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/groups")]
[Authorize]
public class GroupController(IStudentService studentService) : ControllerBase
{
    [HttpPost("{id}/students")]
    [Authorize(Roles = $"{RoleConstants.Trainer},{RoleConstants.Admin}")]
    public async Task<ActionResult<CreateStudentResponse>> CreateStudentAsync(CreateStudentRequest request, Guid id)
    {
        //TODO: protect from other trainers
        
        var result = await studentService.CreateAsync(request, id);

        return result.MatchFirst<ActionResult<CreateStudentResponse>>(
            onValue: response => CreatedAtAction("CreateStudent", response),
            onFirstError: error => error.Type switch
            {
                _ => BadRequest(error.Description)
            });
    }
}