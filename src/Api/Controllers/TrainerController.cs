using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainerJournal.Api.Extensions;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Application.Services.Trainers.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;
using TrainerJournal.Domain.Constants;

namespace TrainerJournal.Api.Controllers;

[ApiController]
[Route("trainers")]
public class TrainerController(
    ITrainerService trainerService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<RegisterUserResponse>> RegisterTrainerAsync(RegisterTrainerRequest request)
    {
        var result = await trainerService.RegisterTrainerAsync(request);

        return result.ToActionResult(
            this, 
            value => CreatedAtAction("RegisterTrainer", value));
    }
}