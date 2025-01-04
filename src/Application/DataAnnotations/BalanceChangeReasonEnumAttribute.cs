using System.ComponentModel.DataAnnotations;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.DataAnnotations;

public class BalanceChangeReasonEnumAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string s)
            if (s.IsBalanceChangeEnum())
                return ValidationResult.Success;
            else 
                return new ValidationResult($"Invalid enum string: {value}");
        
        return ValidationResult.Success;
    }
}