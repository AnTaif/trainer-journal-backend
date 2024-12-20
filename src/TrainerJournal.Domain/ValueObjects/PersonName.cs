using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.ValueObjects;

[ComplexType]
public class PersonName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    
    public PersonName(string firstName, string lastName, string? middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
    
    public PersonName(string fullName)
    {
        var split = SplitFullName(fullName);
        FirstName = split.FirstName;
        LastName = split.LastName;
        MiddleName = split.MiddleName;
    }
    
    public void Set(string fullName)
    {
        var split = SplitFullName(fullName);
        FirstName = split.FirstName;
        LastName = split.LastName;
        MiddleName = split.MiddleName;
    }

    public static PersonName SplitFullName(string fullName)
    {
        var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (names.Length < 2)
            throw new ArgumentException("FullName must consist of at least 2 words");
        
        var lastName = names[0];
        var firstName = names[1];
        var middleName = names.Length > 2 ? names[2] : null;

        return new PersonName(firstName, lastName, middleName);
    }
    
    public override string ToString()
    {
        return MiddleName == null 
            ? $"{LastName} {FirstName}" 
            : $"{LastName} {FirstName} {MiddleName}";
    }
}