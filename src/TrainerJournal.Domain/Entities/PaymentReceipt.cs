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
    
    public bool IsChecked { get; private set; }
    
    public DateTime? CheckDate { get; private set; }

    public bool IsAccepted { get; private set; }

    public string? DeclineComment { get; private set; }

    public void Check(bool isAccepted)
    {
        IsChecked = true;
        IsAccepted = isAccepted;
        CheckDate = DateTime.UtcNow;
    }
}