using TrainerJournal.Application.Services.Users.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Users;

public static class UserExtensions
{
    public static UserInfoDto ToInfoDto(this User user)
    {
        return new UserInfoDto
        {
            FullName = user.FullName.ToString(),
            Gender = user.Gender.ToGenderString(),
            TelegramUsername = user.TelegramUsername
        };
    }

    public static CredentialsDto ToCredentialsDto(this User user)
    {
        return new CredentialsDto
        {
            Login = user.UserName!,
            Password = user.Password
        };
    }
}