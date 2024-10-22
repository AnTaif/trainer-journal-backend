using Microsoft.AspNetCore.Identity;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string? MiddleName { get; private set; }
    
    public Gender Gender { get; private set; }
    
    public string? TelegramUsername { get; private set; }

    public User()
    {
        Id = Guid.NewGuid();
    }
    
    public User(
        string? userName,
        string? email,
        string? phone,
        string firstName, 
        string lastName, 
        string middleName, 
        Gender gender, 
        string? telegramUsername = null)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        PhoneNumber = phone;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Gender = gender;
        TelegramUsername = telegramUsername;
    }
    
    public User(
        string? userName,
        string? email,
        string? phone,
        string fullName, 
        Gender gender, 
        string? telegramUsername = null)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        PhoneNumber = phone;
        var splitFullName = SplitFullName(fullName);
        FirstName = splitFullName.first;
        LastName = splitFullName.last;
        MiddleName = splitFullName.middle;
        Gender = gender;
        TelegramUsername = telegramUsername;
    }
    
    public static (string first, string last, string? middle) SplitFullName(string fullName)
    {
        var names = fullName.Split();
        
        var lastName = names[0];
        var firstName = names[1];
        var middleName = names.Length > 2 ? names[2] : null;

        return (firstName, lastName, middleName);
    }
}