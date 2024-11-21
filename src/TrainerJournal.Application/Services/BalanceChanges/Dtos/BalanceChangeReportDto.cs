namespace TrainerJournal.Application.Services.BalanceChanges.Dtos;

public class BalanceChangeReportDto
{
    public float StartBalance { get; init; }
    
    public float Expenses { get; init; }
    
    public float Payments { get; init; }
    
    public float EndBalance { get; init; }
}