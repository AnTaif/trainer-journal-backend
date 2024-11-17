using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.PaymentReceipts.Dtos;

public class PaymentReceiptDto
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = null!;

    public float Amount { get; init; }

    [Required]
    [DefaultValue("http://localhost:9000/public/guid.jpg")]
    public string ImageUrl { get; init; } = null!;
    
    public DateTime UploadDate { get; init; }
    
    public bool IsChecked { get; init; }
    
    public DateTime? CheckDate { get; init; }

    public bool IsAccepted { get; init; }

    public string? DeclineComment { get; init; }
}