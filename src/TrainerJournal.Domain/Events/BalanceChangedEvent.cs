using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Domain.Events;

public class BalanceChangedEvent(
    Guid studentId,
    float amount,
    float previousBalance,
    BalanceChangeReason balanceChangeReason,
    DateTime date) : DomainEvent
{
    public Guid StudentId { get; } = studentId;

    public float Amount { get; } = amount;

    public float PreviousBalance { get; } = previousBalance;

    public BalanceChangeReason BalanceChangeReason { get; } = balanceChangeReason;

    public DateTime Date { get; } = date;
}