using System.ComponentModel.DataAnnotations;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.DataAnnotations;

public class GenderEnumAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string s)
            if (s.IsGenderEnum())
                return ValidationResult.Success;
            else 
                return new ValidationResult($"Invalid enum string: {value}");
        
        return ValidationResult.Success;
    }
}