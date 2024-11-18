using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Domain.Entities;

public class PaymentReceipt(
    Guid studentId,
    float amount,
    Guid imageId,
    DateTime uploadDate)
    : Entity<Guid>(Guid.NewGuid())
{
    public Guid StudentId { get; private set; } = studentId;
    public virtual Student Student { get; private set; } = null!;
    
    public float Amount { get; private set; } = amount;

    public Guid ImageId { get; private set; } = imageId;
    public SavedFile Image { get; private set; } = null!;

    public DateTime UploadDate { get; private set; } = uploadDate;
    
    public bool IsVerified { get; private set; }
    
    public DateTime? VerificationDate { get; private set; }

    public bool IsAccepted { get; private set; }

    public string? DeclineComment { get; private set; }

    public void Verify(bool isAccepted, string? declineComment)
    {
        if (IsAccepted == isAccepted) return;
        
        VerificationDate = DateTime.UtcNow;
        if (IsAccepted)
        {
            Student.UpdateBalance(-Amount, BalanceChangeReason.PaymentRejection, VerificationDate.Value);
        }
        
        IsVerified = true;
        IsAccepted = isAccepted;

        if (IsAccepted)
        {
            Student.UpdateBalance(Amount, BalanceChangeReason.Payment, VerificationDate.Value);
        }
        else
            DeclineComment = declineComment ?? "";
    }
}