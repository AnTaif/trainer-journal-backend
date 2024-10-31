using ErrorOr;
using idunno.Password;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Constants;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;
using TrainerJournal.Domain.Services;

namespace TrainerJournal.Application.Services.Users;

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

        return new GetUserInfoResponse(user.Id, user.ToInfoDto(), student?.ToInfoDto(), trainer?.ToInfoDto());
    }

    public async Task<ErrorOr<CreateUserResponse>> CreateAsync(CreateUserRequest request)
    {
        var username = GenerateUsername(request.FullName);
        var password = GeneratePassword();
        
        var existedUser = await userManager.FindByNameAsync(username);
        if (existedUser != null)
        {
            logger.LogWarning("User with username {username} already exists", username);
            return Error.Failure("Student.Create", "User is already exists");
        }

        var user = new User(username, request.Email, request.Phone, new PersonName(request.FullName), request.Gender.ToGenderEnum());

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to create user: {username}", username);
            return Error.Failure("User not created");
        }

        await userManager.AddToRoleAsync(user, Roles.User);

        return new CreateUserResponse(user.Id, password, user.FullName.ToString(), user.UserName!, user.Email, user.PhoneNumber,
            user.Gender.ToGenderString(), user.TelegramUsername);
    }

    private string GenerateUsername(string fullName)
    {
        var latinSplit = PersonName.SplitFullName(CyrillicTextConverter.ConvertToLatin(fullName));
        var username = latinSplit.MiddleName != null 
            ? $"{latinSplit.FirstName.First()}.{latinSplit.MiddleName.First()}.{latinSplit.LastName}" 
            : $"{latinSplit.FirstName}.{latinSplit.LastName}";
        
        var count = userManager.Users.Count(u => u.UserName!.Contains(username));
        return username + (count == 0 ? string.Empty : count);
    }

    private static string GeneratePassword()
    {
        var generator = new PasswordGenerator();
        return generator.Generate(10, 4, 0, false, true);
    }
}