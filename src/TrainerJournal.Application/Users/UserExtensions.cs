using TrainerJournal.Application.Users.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Users;

public static class UserExtensions
{
    public static UserInfoDto ToInfoDto(this User user)
    {
        return new UserInfoDto(user.Id, user.GetFullName(), user.UserName, user.Email, user.PhoneNumber,
            user.Gender.ToGenderString(), user.TelegramUsername);
    }
}