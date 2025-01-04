using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public static class PaymentReceiptExtensions
{
    public static PaymentReceiptDto ToDto(this PaymentReceipt paymentReceipt)
    {
        return new PaymentReceiptDto
        {
            Id = paymentReceipt.Id,
            Student = paymentReceipt.Student.ToShortDto(),
            Amount = paymentReceipt.Amount,
            ImageUrl = paymentReceipt.Image.Url,
            UploadDate = paymentReceipt.UploadDate,
            IsVerified = paymentReceipt.IsVerified,
            VerificationDate = paymentReceipt.VerificationDate,
            IsAccepted = paymentReceipt.IsAccepted,
            DeclineComment = paymentReceipt.DeclineComment
        };
    }
}