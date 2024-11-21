using TrainerJournal.Application.Services.BalanceChanges.Dtos;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.BalanceChanges;

public interface IBalanceChangeService
{
    public Task<Result<List<BalanceChangeDto>>> GetStudentBalanceChanges(string username, DateTime start, DateTime end);
    
    public Task<Result<BalanceChangeReportDto>> GetStudentBalanceChangeReportAsync(string username, DateTime start,
        DateTime end);
}