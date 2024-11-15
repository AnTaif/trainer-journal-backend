using ErrorOr;
using idunno.Password;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Application.Services.Users.Dtos;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
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
    public async Task<ErrorOr<FullInfoDto>> GetMyInfoAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return Error.NotFound("User not found");

        var student = await studentRepository.GetByUserIdAsync(id);
        var trainer = await trainerRepository.GetByUserIdAsync(id);

        if (student == null && trainer == null)
            logger.LogError("User with an id {id} has neither a trainer nor a student account", id);
        else if (student != null && trainer != null)
            logger.LogError("User with an id {id} has both accounts: trainer and student", id);

        return new FullInfoDto
        {
            Username = user.UserName!,
            UserInfo = user.ToInfoDto(),
            Credentials = user.ToCredentialsDto(),
            StudentInfo = student?.ToInfoDto(),
            TrainerInfo = trainer?.ToInfoDto()
        };
    }

    public async Task<ErrorOr<FullInfoDto>> GetInfoByUsernameAsync(Guid requestedUserId, string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return Error.NotFound("User not found");

        var student = await studentRepository.GetByUserIdAsync(user.Id);
        var trainer = await trainerRepository.GetByUserIdAsync(user.Id);

        if (student == null && trainer == null)
            logger.LogError("User with an id {id} has neither a trainer nor a student account", user.Id);
        else if (student != null && trainer != null)
            logger.LogError("User with an id {id} has both accounts: trainer and student", user.Id);

        return new FullInfoDto
        {
            Username = user.UserName!,
            UserInfo = user.ToInfoDto(),
            Credentials = await CanViewCredentials(requestedUserId, user) ? user.ToCredentialsDto() : null,
            StudentInfo = student?.ToInfoDto(),
            TrainerInfo = trainer?.ToInfoDto()
        };
    }

    private async Task<bool> CanViewCredentials(Guid requestedUserId, User user)
    {
        var requestedUser = await userManager.FindByIdAsync(requestedUserId.ToString());
        if (requestedUser == null) throw new Exception("Cannot find logged in user");
        
        var requestedUserRoles = await userManager.GetRolesAsync(requestedUser);
        if (requestedUserRoles.Contains(Roles.Trainer) || requestedUserRoles.Contains(Roles.Admin)) return true;

        if (user.Id == requestedUserId) return true;
        
        return false;
    }

    public async Task<ErrorOr<FullInfoWithoutCredentialsDto>> UpdateMyInfoAsync(Guid id, UpdateFullInfoRequest request)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return Error.NotFound("User not found");

        var student = await studentRepository.GetByUserIdAsync(id);
        if (student == null && request.StudentInfo != null)
            return Error.Failure("This user is not a student");
        
        var trainer = await trainerRepository.GetByUserIdAsync(id);
        if (trainer == null && request.TrainerInfo != null)
            return Error.Failure("This user is not a trainer");
        
        if (request.UserInfo != null)
        {
            var userReq = request.UserInfo;
            var fullName = userReq.FullName != null ? new PersonName(userReq.FullName) : null;
            user.Update(fullName, userReq.Gender?.ToGenderEnum(), userReq.TelegramUsername);
        }

        if (request.StudentInfo != null)
        {
            var studentReq = request.StudentInfo;
            student!.Update(studentReq.BirthDate, studentReq.SchoolGrade, studentReq.Address, studentReq.Kyu);
            
            var contacts = studentReq.Contacts?
                .Select(c => c.ToEntity())
                .ToList();
            await studentRepository.UpdateContactsAsync(student, contacts);
        }

        if (request.TrainerInfo != null)
        {
            var trainerReq = request.TrainerInfo;
            trainer!.Update(trainerReq.Phone, trainerReq.Email);
        }
        
        await userManager.UpdateAsync(user);
        return new FullInfoWithoutCredentialsDto
        {
            Username = user.UserName!,
            UserInfo = user.ToInfoDto(),
            StudentInfo = student?.ToInfoDto(),
            TrainerInfo = trainer?.ToInfoDto()
        };
    }

    public async Task<ErrorOr<FullInfoWithoutCredentialsDto>> UpdateStudentInfoAsync(
        Guid userId, string username, UpdateUserStudentInfoRequest request)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return Error.NotFound("User not found");
        
        var student = await studentRepository.GetByUserIdAsync(user.Id);
        if (student == null) return Error.Failure("This user is not a student");

        if (request.UserInfo != null)
        {
            var userReq = request.UserInfo;
            var fullName = userReq.FullName != null ? new PersonName(userReq.FullName) : null;
            user.Update(fullName, userReq.Gender?.ToGenderEnum(), userReq.TelegramUsername);
        }
        
        if (request.StudentInfo != null)
        {
            var studentRequest = request.StudentInfo;
            student.Update(studentRequest.BirthDate, studentRequest.SchoolGrade, studentRequest.Address, studentRequest.Kyu);
                
            var contacts = studentRequest.Contacts?
                .Select(c => c.ToEntity())
                .ToList();
            await studentRepository.UpdateContactsAsync(student, contacts);
        }
        
        await studentRepository.SaveChangesAsync();
        return new FullInfoWithoutCredentialsDto
        {
            Username = user.UserName!,
            UserInfo = user.ToInfoDto(),
            StudentInfo = student.ToInfoDto()
        };
    }

    public async Task<ErrorOr<User>> CreateAsync(string fullName, Gender gender)
    {
        var username = GenerateUsername(fullName);
        var password = GeneratePassword();
        
        var existedUser = await userManager.FindByNameAsync(username);
        if (existedUser != null)
        {
            logger.LogWarning("User with username {username} already exists", username);
            return Error.Failure("User is already exists");
        }

        var user = new User(username, password, new PersonName(fullName), gender);

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to create user: {username}", username);
            return Error.Failure("User not created");
        }

        await userManager.AddToRoleAsync(user, Roles.User);

        return user;
    }

    public string GenerateUsername(string fullName)
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