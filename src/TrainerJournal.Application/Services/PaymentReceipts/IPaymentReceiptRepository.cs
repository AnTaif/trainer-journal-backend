using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public interface IPaymentReceiptRepository 
{
    public Task<PaymentReceipt?> GetByIdAsync(Guid id);

    public Task<List<PaymentReceipt>> GetAllByUserIdAsync(Guid userId);
    
    public Task<List<PaymentReceipt>> GetVerifiedByUserIdAsync(Guid userId, bool verified);
    
    public Task<List<PaymentReceipt>> GetByStudentUsernameAsync(string username);
    
    public Task<List<PaymentReceipt>> GetVerifiedByStudentUsernameAsync(string username, bool verified);
    
    public void AddPaymentReceipt(PaymentReceipt paymentReceipt);
    
    public Task SaveChangesAsync();
}