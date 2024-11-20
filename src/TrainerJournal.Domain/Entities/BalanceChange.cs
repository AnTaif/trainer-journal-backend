using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Domain.Entities;

public class BalanceChange(Guid studentId, float amount, float previousBalance, BalanceChangeReason reason, DateTime date)
    : Entity<Guid>(Guid.NewGuid())
{
    public Guid StudentId { get; private set; } = studentId;
    public Student Student { get; private set; } = null!;
    
    public float Amount { get; private set; } = amount;

    public float PreviousBalance { get; private set; } = previousBalance;

    public BalanceChangeReason Reason { get; private set; } = reason;
    
    public DateTime Date { get; private set; } = date;

    public bool IsExpense()
        => Reason is BalanceChangeReason.MarkAttendance or BalanceChangeReason.UnmarkAttendance;

    public bool IsPayment()
        => Reason is BalanceChangeReason.Payment or BalanceChangeReason.PaymentRejection;

    public float GetAfterBalance() => PreviousBalance + Amount;
}