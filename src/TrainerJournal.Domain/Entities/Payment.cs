using TrainerJournal.Domain.Entities.Abstract;

namespace TrainerJournal.Domain.Entities;

public class Payment : Entity<Guid>
{
    public Guid StudentId { get; private set; }
    public virtual Student Student { get; private set; } = null!;
    
    public float Amount { get; private set; }
    
    public float PreviousBalance { get; private set; }
    
    public string ReceiptUrl { get; private set; }
    
    public DateTime Date { get; private set; }
    
    public bool IsChecked { get; private set; }
    
    public bool IsApproved { get; private set; }
    
    public string DeclineComment { get; private set; }
    
    public Payment(
        Guid studentId, 
        float amount, 
        float previousBalance, 
        string receiptUrl, 
        DateTime date, 
        bool isChecked, 
        bool isApproved, 
        string declineComment) : base(Guid.NewGuid())
    {
        StudentId = studentId;
        Amount = amount;
        PreviousBalance = previousBalance;
        ReceiptUrl = receiptUrl;
        Date = date;
        IsChecked = isChecked;
        IsApproved = isApproved;
        DeclineComment = declineComment;
    }
}