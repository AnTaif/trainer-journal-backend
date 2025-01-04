using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.PaymentReceipts;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class PaymentReceiptRepository(AppDbContext context) : BaseRepository(context), IPaymentReceiptRepository
{
    private DbSet<PaymentReceipt> receipts => dbContext.PaymentReceipts;
    
    public async Task<PaymentReceipt?> FindByIdAsync(Guid id)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<PaymentReceipt>> SelectByUserIdAsync(Guid userId)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Student)
                .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(p => p.Student.Id == userId || p.Student.Groups.Any(g => g.TrainerId == userId))
            .OrderByDescending(p => p.UploadDate) //TODO: Изменить на время последнего изменения? 
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> SelectByUserIdAsync(Guid userId, bool verified)
    {
        return await receipts
            .Include(r => r.Student)
                .ThenInclude(s => s.User)
            .Include(r => r.Student)
                .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(p => (p.Student.Id == userId || p.Student.Groups.Any(g => g.TrainerId == userId))
                                        && p.IsVerified == verified)
            .OrderByDescending(p => p.UploadDate) //TODO: Изменить на время последнего изменения? 
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> SelectByStudentUsernameAsync(string username)
    {
        return await receipts
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .Include(r => r.Image)
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Where(r => r.Student.User.UserName == username)
            .OrderByDescending(p => p.UploadDate) //TODO: Изменить на время последнего изменения? 
            .ToListAsync();
    }

    public async Task<List<PaymentReceipt>> SelectByStudentUsernameAsync(string username, bool verified)
    {
        return await receipts
            .Include(r => r.Student)
            .ThenInclude(s => s.User)
            .Include(r => r.Student)
            .ThenInclude(s => s.Groups)
            .Include(r => r.Image)
            .Where(r => r.Student.User.UserName == username && r.IsVerified == verified)
            .OrderByDescending(p => p.UploadDate) //TODO: Изменить на время последнего изменения? 
            .ToListAsync();
    }

    public void AddPaymentReceipt(PaymentReceipt paymentReceipt)
    {
        receipts.Add(paymentReceipt);
    }
}