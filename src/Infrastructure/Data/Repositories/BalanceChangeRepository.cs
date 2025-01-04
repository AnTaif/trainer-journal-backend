using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class BalanceChangeRepository(AppDbContext context) : BaseRepository(context), IBalanceChangeRepository
{
    private DbSet<BalanceChange> balanceChanges => dbContext.BalanceChanges;
    
    public async Task<List<BalanceChange>> SelectByStudentIdAsync(Guid studentId, DateTime start, DateTime end)
    {
        return await balanceChanges
            .OrderBy(b => b.Date)
            .Where(b => 
                b.StudentId == studentId
                && start <= b.Date 
                && b.Date <= end)
            .ToListAsync();
    }

    public async Task<BalanceChange?> FindLastByStudentIdAsync(Guid studentId, DateTime date)
    {
        return await balanceChanges
            .OrderByDescending(b => b.Date)
            .FirstOrDefaultAsync(b => 
                b.StudentId == studentId 
                && b.Date <= date);
    }

    public async Task<BalanceChange?> FindOnDateAsync(Guid studentId, DateTime date)
    {
        return await balanceChanges
            .OrderBy(b => b.Date)
            .FirstOrDefaultAsync(b => 
                b.StudentId == studentId 
                && b.Date >= date);
    }

    public void Add(BalanceChange balanceChange)
    {
        balanceChanges.Add(balanceChange);
    }
}