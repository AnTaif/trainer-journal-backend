using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.BalanceChanges;

public interface IBalanceChangeRepository : IUnitOfWork
{
    Task<List<BalanceChange>> SelectByStudentIdAsync(Guid studentId, DateTime start, DateTime end);

    Task<BalanceChange?> FindLastByStudentIdAsync(Guid studentId, DateTime date);
    
    Task<BalanceChange?> FindOnDateAsync(Guid studentId, DateTime date);
    
    void Add(BalanceChange balanceChange);
}