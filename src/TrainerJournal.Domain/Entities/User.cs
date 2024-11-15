using Microsoft.AspNetCore.Identity;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public PersonName FullName { get; set; } = null!;
    public Gender Gender { get; private set; }
    public string? TelegramUsername { get; private set; }

    /// <summary>
    /// Нужно выводить пароль в личном кабинете, решили просто хранить пароль в открытую,
    /// возможно потом что-нибудь придумаем ^_^
    /// </summary>
    public string Password { get; private set; } = null!;

    public User() { Id = Guid.NewGuid(); }
    
    public User(string userName, string password, PersonName fullName, Gender gender)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Password = password;
        FullName = fullName;
        Gender = gender;
    }

    public void Update(PersonName? fullname, Gender? gender, string? telegramUsername)
    {
        FullName = fullname ?? FullName;
        Gender = gender ?? Gender;
        TelegramUsername = telegramUsername ?? TelegramUsername;
    }
}