using Microsoft.AspNetCore.Identity;

namespace TrainerJournal.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string MiddleName { get; private set; }
    
    public Gender Gender { get; private set; }
    
    public string? TelegramUsername { get; private set; }

    public User()
    {
        Id = Guid.NewGuid();
    }
    
    public User(
        string email,
        string phone,
        string firstName, 
        string lastName, 
        string middleName, 
        Gender gender, 
        string? telegramUsername = null)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = email;
        PhoneNumber = phone;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Gender = gender;
        TelegramUsername = telegramUsername;
    }
}