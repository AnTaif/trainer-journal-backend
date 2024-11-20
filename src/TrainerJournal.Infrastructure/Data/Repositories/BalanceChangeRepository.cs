using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class BalanceChangeRepository(AppDbContext context) : BaseRepository(context), IBalanceChangeRepository
{
    private DbSet<BalanceChange> balanceChanges => dbContext.BalanceChanges;
    
    public async Task<List<BalanceChange>> GetStudentBalanceChangesAsync(Guid studentId, DateTime start, DateTime end)
    {
        return await balanceChanges
            .Where(b => b.StudentId == studentId
                        && start <= b.Date && b.Date <= end)
            .OrderBy(b => b.Date)
            .ToListAsync();
    }

    public async Task<BalanceChange?> GetStudentLeftEdgeBalanceChangeAsync(
        Guid studentId, DateTime start, DateTime end)
    {
        return await balanceChanges
            .OrderByDescending(b => b.Date)
            .FirstOrDefaultAsync(b => b.StudentId == studentId && b.Date <= start);
    }

    public async Task<List<BalanceChange>> GetStudentBalanceChangesWithReasonAsync(
        Guid studentId, BalanceChangeReason reason, DateTime start, DateTime end)
    {
        return await balanceChanges
            .Where(b => b.StudentId == studentId
                        && b.Reason == reason
                        && start <= b.Date && b.Date <= end)
            .OrderBy(b => b.Date)
            .ToListAsync();
    }

    public async Task<BalanceChange?> GetStudentBalanceChangeOnDateAsync(Guid studentId, DateTime date)
    {
        return await balanceChanges
            .OrderBy(b => b.Date)
            .FirstOrDefaultAsync(b => b.StudentId == studentId && b.Date >= date);
    }

    public void Add(BalanceChange balanceChange)
    {
        balanceChanges.Add(balanceChange);
    }
}