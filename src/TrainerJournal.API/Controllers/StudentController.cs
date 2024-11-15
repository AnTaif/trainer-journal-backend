using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TrainerJournal.API.Extensions;
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
    IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentItemDto>>> GetStudents([FromQuery] bool withGroup = true)
    {
        var trainerId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (trainerId == null) return Unauthorized();

        var result = await studentService.GetStudentsByTrainerAsync(Guid.Parse(trainerId), withGroup);
        return this.ToActionResult(result, Ok);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateStudentResponse>> CreateGroupStudentAsync(CreateStudentRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (userId == null) return Unauthorized();

        var result = await studentService.CreateAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction("CreateGroupStudent", value));
    }
}