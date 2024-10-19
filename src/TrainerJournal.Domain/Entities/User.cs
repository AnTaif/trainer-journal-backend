namespace TrainerJournal.Domain.Entities;

public class User
{
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string MiddleName { get; private set; }
    
    public Gender Gender { get; private set; }
    
    public string? TelegramUsername { get; private set; }

    public User(string firstName, string lastName, string middleName, Gender gender, string? telegramUsername = null)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Gender = gender;
        TelegramUsername = telegramUsername;
    }
}