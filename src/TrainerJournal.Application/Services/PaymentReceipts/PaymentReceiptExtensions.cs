using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public static class PaymentReceiptExtensions
{
    public static PaymentReceiptDto ToDto(this PaymentReceipt paymentReceipt)
    {
        return new PaymentReceiptDto
        {
            Username = paymentReceipt.Student.User.UserName!,
            Amount = paymentReceipt.Amount,
            ImageUrl = paymentReceipt.Image.Url,
            UploadDate = paymentReceipt.UploadDate,
            IsChecked = paymentReceipt.IsChecked,
            CheckDate = paymentReceipt.CheckDate,
            IsAccepted = paymentReceipt.IsAccepted,
            DeclineComment = paymentReceipt.DeclineComment
        };
    }
}