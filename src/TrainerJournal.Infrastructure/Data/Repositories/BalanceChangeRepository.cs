using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Finance;
using TrainerJournal.Domain.Entities;
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
            .ToListAsync();
    }

    public void Add(BalanceChange balanceChange)
    {
        balanceChanges.Add(balanceChange);
    }
}