using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Finance;

public interface IBalanceChangeRepository
{
    public Task<List<BalanceChange>> GetStudentBalanceChangesAsync(Guid studentId, DateTime start, DateTime end);
    
    public void Add(BalanceChange balanceChange);
    
    public Task SaveChangesAsync();
}