using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.DataAnnotations;

public class MinimumWordsCountAttribute(int count) : ValidationAttribute
{
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string s)
            if (s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length >= count) 
                return ValidationResult.Success;
        
        return new ValidationResult($"Value must consist of at least {count} words");
    }
}