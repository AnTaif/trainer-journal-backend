using Microsoft.AspNetCore.Identity;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    public PersonName FullName { get; set; }
    public Gender Gender { get; private set; }
    public string? TelegramUsername { get; private set; }

    public User() { Id = Guid.NewGuid(); }
    
    public User(string? userName, PersonName fullName, Gender gender)
    {
        Id = Guid.NewGuid();
        UserName = userName;
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