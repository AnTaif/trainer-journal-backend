using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class PaymentReceiptRepository(AppDbContext context) : BaseRepository(context), IPaymentReceiptRepository
{
    private DbSet<PaymentReceipt> receipts => dbContext.PaymentReceipts;
    
    public async Task<PaymentReceipt?> GetByIdAsync(Guid id)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Image)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<PaymentReceipt>> GetByStudentUsernameAsync(string username)
    {
        return await receipts
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .Include(r => r.Image)
            .Where(r => r.Student.User.UserName == username)
            .ToListAsync();
    }

    public void AddPaymentReceipt(PaymentReceipt paymentReceipt)
    {
        receipts.Add(paymentReceipt);
    }
}