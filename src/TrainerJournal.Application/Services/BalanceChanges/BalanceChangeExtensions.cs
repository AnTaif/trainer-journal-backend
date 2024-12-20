using TrainerJournal.Application.Services.BalanceChanges.Dtos;
using TrainerJournal.Application.Services.BalanceChanges.Dtos.Responses;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.BalanceChanges;

public static class BalanceChangeExtensions
{
    public static BalanceChangeDto ToDto(this BalanceChange balanceChange, string username)
    {
        return new BalanceChangeDto
        {
            Username = username,
            Amount = balanceChange.Amount,
            PreviousBalance = balanceChange.PreviousBalance,
            AfterBalance = balanceChange.PreviousBalance + balanceChange.Amount,
            Reason = balanceChange.Reason.ToBalanceChangeString(),
            Date = balanceChange.Date
        };
    }
    
    public static GetStudentBalanceChangeResponse ToStudentResponse(this BalanceChange balanceChange)
    {
        return new GetStudentBalanceChangeResponse
        {
            Amount = balanceChange.Amount,
            PreviousBalance = balanceChange.PreviousBalance,
            AfterBalance = balanceChange.PreviousBalance + balanceChange.Amount,
            Reason = balanceChange.Reason.ToBalanceChangeString(),
            Date = balanceChange.Date
        };
    }
}