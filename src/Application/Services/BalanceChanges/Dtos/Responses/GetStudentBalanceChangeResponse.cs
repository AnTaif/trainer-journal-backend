using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.BalanceChanges.Dtos.Responses;

public class GetStudentBalanceChangeResponse
{
    public float Amount { get; init; }
    
    public float PreviousBalance { get; init; }
    
    public float AfterBalance { get; init; }

    [Required]
    [DefaultValue("Оплата")]
    public string Reason { get; init; } = null;
    
    public DateTime Date { get; init; }
}