using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public interface IPaymentReceiptService
{
    public Task<Result<PaymentReceiptDto>> GetByIdAsync(Guid id);

    public Task<Result<List<PaymentReceiptDto>>>
        GetByStudentUsernameAsync(Guid userId, string username, bool? verified);

    public Task<Result<List<PaymentReceiptDto>>> GetByUserIdAsync(Guid userId, bool? verified);

    public Task<Result<PaymentReceiptDto>> UploadAsync(Guid userId, Stream imageStream, string imageName,
        UploadPaymentReceiptRequest request);

    public Task<Result> DeleteAsync(Guid userId, Guid id);

    public Task<Result<PaymentReceiptDto>> VerifyAsync(Guid id, VerifyPaymentReceiptRequest request);
}