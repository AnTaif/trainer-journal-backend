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
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<PaymentReceipt>> GetAllByUserIdAsync(Guid userId)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Student)
                .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(p => p.Student.UserId == userId || p.Student.Groups.Any(g => g.TrainerId == userId))
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> GetVerifiedByUserIdAsync(Guid userId, bool verified)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Student)
                .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(p => (p.Student.UserId == userId || p.Student.Groups.Any(g => g.TrainerId == userId))
                                        && p.IsVerified == verified)
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> GetByStudentUsernameAsync(string username)
    {
        return await receipts
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .Include(r => r.Image)
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Where(r => r.Student.User.UserName == username)
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> GetVerifiedByStudentUsernameAsync(string username, bool verified)
    {
        return await receipts
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(r => r.Student.User.UserName == username && r.IsVerified == verified)
            .ToListAsync();
    }

    public void AddPaymentReceipt(PaymentReceipt paymentReceipt)
    {
        receipts.Add(paymentReceipt);
    }
}