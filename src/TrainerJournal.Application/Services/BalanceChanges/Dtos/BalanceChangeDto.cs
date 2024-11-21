using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.BalanceChanges.Dtos;

public class BalanceChangeDto
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = null!;

    public float Amount { get; init; }
    
    public float PreviousBalance { get; init; }
    
    public float AfterBalance { get; init; }

    [Required]
    [DefaultValue("Оплата")]
    public string Reason { get; init; } = null;
    
    public DateTime Date { get; init; }
}