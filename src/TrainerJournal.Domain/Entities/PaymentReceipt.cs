using TrainerJournal.Domain.Common;

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

    public void Verify(bool isAccepted)
    {
        IsVerified = true;
        IsAccepted = isAccepted;
        VerificationDate = DateTime.UtcNow;
    }
}