using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Trainers;
using TrainerJournal.Application.Users.Dtos.Responses;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Users;

public class UserService(
    UserManager<User> userManager,
    IStudentRepository studentRepository,
    ITrainerRepository trainerRepository,
    ILogger<UserService> logger) : IUserService
{
    public async Task<ErrorOr<GetUserInfoResponse>> GetInfoAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            logger.LogWarning("User not found by id: {id}", id);
            return Error.NotFound("User.GetInfo", "User not found");
        }

        var student = await studentRepository.GetByUserIdAsync(id);
        var trainer = await trainerRepository.GetByUserIdAsync(id);

        if (student == null && trainer == null)
            logger.LogWarning("User with an id {id} has neither a trainer nor a student account", id);
        else if (student != null && trainer != null)
            logger.LogWarning("User with an id {id} has both accounts: trainer and student", id);

        return new GetUserInfoResponse(user.ToInfoDto(), student?.ToInfoDto(), trainer?.ToInfoDto());
    }
}