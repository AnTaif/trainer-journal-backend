using TrainerJournal.Application.Services.BalanceChanges.Dtos;
using TrainerJournal.Application.Services.BalanceChanges.Dtos.Responses;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.BalanceChanges;

public interface IBalanceChangeService
{
    public Task<Result<List<GetStudentBalanceChangeResponse>>> GetStudentBalanceChanges(string username, DateTime start, DateTime end);
    
    public Task<Result<BalanceChangeReportDto>> GetStudentBalanceChangeReportAsync(string username, DateTime start,
        DateTime end);
}