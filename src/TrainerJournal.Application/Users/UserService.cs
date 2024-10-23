using ErrorOr;
using idunno.Password;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Students;
using TrainerJournal.Application.Trainers;
using TrainerJournal.Application.Users.Dtos.Responses;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Services;

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
    
    public static string GenerateUsername(string fullname, UserManager<User> userManager)
    {
        var latinSplit = User.SplitFullName(CyrillicTextConverter.ConvertToLatin(fullname));

        var userName = latinSplit.MiddleName != null 
            ? $"{latinSplit.FirstName.First()}.{latinSplit.MiddleName.First()}.{latinSplit.LastName}" 
            : $"{latinSplit.FirstName}.{latinSplit.LastName}";

        var count = userManager.Users.Count(u => u.UserName!.Contains(userName));
        if (count == 0)
            return userName;

        return userName + count;
    }

    public static string GeneratePassword()
    {
        var generator = new PasswordGenerator();
        return generator.Generate(10, 4, 0, false, true);
    }
}