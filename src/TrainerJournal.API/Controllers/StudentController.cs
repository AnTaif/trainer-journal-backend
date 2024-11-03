using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.API.Controllers;

[ApiController]
[Route("/students")]
[Authorize]
public class StudentController(
    IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentItemDto>>> GetStudents([FromQuery] bool withGroup)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = await studentService.GetStudentsByTrainerAsync(Guid.Parse(trainerId), withGroup);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPut("{id}/group")]
    [Authorize(Roles = Roles.Trainer)]
    public async Task<ActionResult<StudentInfoDto>> ChangeStudentGroupAsync(Guid id, ChangeStudentGroupRequest request)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = await studentService.ChangeStudentGroupAsync(id, Guid.Parse(trainerId), request);
        return this.ToActionResult(result, Ok);
    }
}