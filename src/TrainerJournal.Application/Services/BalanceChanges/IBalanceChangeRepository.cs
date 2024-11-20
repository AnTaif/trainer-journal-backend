using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.BalanceChanges;

public interface IBalanceChangeRepository
{
    public Task<List<BalanceChange>> GetStudentBalanceChangesAsync(Guid studentId, DateTime start, DateTime end);

    public Task<BalanceChange?> GetStudentLeftEdgeBalanceChangeAsync(
        Guid studentId, DateTime start, DateTime end);

    public Task<List<BalanceChange>> GetStudentBalanceChangesWithReasonAsync(Guid studentId, BalanceChangeReason reason,
        DateTime start, DateTime end);
    
    public Task<BalanceChange?> GetStudentBalanceChangeOnDateAsync(Guid studentId, DateTime date);
    
    public void Add(BalanceChange balanceChange);
    
    public Task SaveChangesAsync();
}