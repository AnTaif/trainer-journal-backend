using ErrorOr;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos;
using TrainerJournal.Application.Services.PaymentReceipts.Dtos.Requests;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public interface IPaymentReceiptService
{
    public Task<ErrorOr<PaymentReceiptDto>> GetByIdAsync(Guid id);
    
    public Task<ErrorOr<List<PaymentReceiptDto>>> GetByStudentUsernameAsync(Guid userId, string username, bool? verified);

    public Task<ErrorOr<List<PaymentReceiptDto>>> GetByUserIdAsync(Guid userId, bool? verified);
    
    public Task<ErrorOr<PaymentReceiptDto>> UploadAsync(Guid userId, Stream imageStream, string imageName,
        UploadPaymentReceiptRequest request);

    public Task<ErrorOr<bool>> DeleteAsync(Guid userId, Guid id);
}
