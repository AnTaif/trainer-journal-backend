using ErrorOr;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public class PaymentReceiptService(
    IFileStorage fileStorage,
    ISavedFileRepository savedFileRepository,
    IPaymentReceiptRepository paymentReceiptRepository) : IPaymentReceiptService
{
    public async Task<ErrorOr<PaymentReceiptDto>> UploadAsync(Guid userId, Stream imageStream, string imageName,
        UploadPaymentReceiptRequest request)
    {
        var destName = Guid.NewGuid() + Path.GetExtension(imageName);
        var url = await fileStorage.UploadAsync(imageStream, "Public", destName);

        var file = new SavedFile(destName, url, FileType.PaymentReceipt);

        var newPaymentReceipt = new PaymentReceipt(userId, request.Amount, file.Id, DateTime.UtcNow);

        savedFileRepository.Add(file);
        paymentReceiptRepository.AddPaymentReceipt(newPaymentReceipt);
        await paymentReceiptRepository.SaveChangesAsync();

        var paymentReceipt = await paymentReceiptRepository.GetByIdAsync(newPaymentReceipt.Id);
        return paymentReceipt!.ToDto();
    }

    public async Task<ErrorOr<List<PaymentReceiptDto>>> GetByStudentUsernameAsync(Guid userId, string username)
    {
        var paymentReceipts = await paymentReceiptRepository.GetByStudentUsernameAsync(username);

        return paymentReceipts.Select(p => p.ToDto()).ToList();
    }
}