using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public interface IPaymentReceiptRepository : IUnitOfWork
{
    Task<PaymentReceipt?> FindByIdAsync(Guid id);

    Task<List<PaymentReceipt>> SelectByUserIdAsync(Guid userId);
    
    Task<List<PaymentReceipt>> SelectByUserIdAsync(Guid userId, bool verified);
    
    Task<List<PaymentReceipt>> SelectByStudentUsernameAsync(string username);
    
    Task<List<PaymentReceipt>> SelectByStudentUsernameAsync(string username, bool verified);
    
    void AddPaymentReceipt(PaymentReceipt paymentReceipt);
}