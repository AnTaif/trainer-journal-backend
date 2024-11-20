using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.Services.Students.Dtos;

namespace TrainerJournal.Application.Services.PaymentReceipts.Dtos;

public class PaymentReceiptDto
{
    public Guid Id { get; init; }

    public StudentShortDto Student { get; init; }
    
    public float Amount { get; init; }

    [Required]
    [DefaultValue("http://localhost:9000/public/guid.jpg")]
    public string ImageUrl { get; init; } = null!;
    
    public DateTime UploadDate { get; init; }
    
    public bool IsVerified { get; init; }
    
    public DateTime? VerificationDate { get; init; }

    public bool IsAccepted { get; init; }

    public string? DeclineComment { get; init; }
}