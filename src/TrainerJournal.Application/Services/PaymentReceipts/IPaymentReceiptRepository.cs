using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.PaymentReceipts;

public interface IPaymentReceiptRepository 
{
    public Task<PaymentReceipt?> GetByIdAsync(Guid id);
    
    public Task<List<PaymentReceipt>> GetByStudentUsernameAsync(string username);
    
    public void AddPaymentReceipt(PaymentReceipt paymentReceipt);
    
    public Task SaveChangesAsync();
}