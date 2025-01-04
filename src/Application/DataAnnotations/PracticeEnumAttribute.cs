using System.ComponentModel.DataAnnotations;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.DataAnnotations;

public class PracticeEnumAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string s)
            if (s.IsPracticeEnum())
                return ValidationResult.Success;
            else 
                return new ValidationResult($"Invalid enum string: {value}");
        
        return ValidationResult.Success;
    }
}