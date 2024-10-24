using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("students")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = $"{Roles.Trainer},{Roles.Admin}")]
    public async Task<ActionResult<CreateStudentResponse>> CreateStudentAsync(CreateStudentRequest request)
    {
        //TODO: protect from other trainers
        
        var result = await studentService.CreateAsync(request);

        return result.MatchFirst<ActionResult<CreateStudentResponse>>(
            onValue: response => CreatedAtAction("CreateStudent", response),
            onFirstError: error => error.Type switch
            {
                _ => BadRequest(error.Description)
            });
    }
}