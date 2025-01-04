namespace TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;

public class VerifyPaymentReceiptRequest
{
    public bool IsAccepted { get; init; }
    
    public string? DeclineComment { get; init; }
}