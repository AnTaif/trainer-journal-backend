namespace TrainerJournal.Application.Services.BalanceChanges;

public interface IBalanceChangeManager
{
    public Task<float> GetStudentBalanceOnDate(Guid userId, DateTime date);

    public Task<(float StartBalance, float Expenses, float Payments, float EndBalance)> GetStudentBalanceReport(
        Guid userId, DateTime start, DateTime end);
}