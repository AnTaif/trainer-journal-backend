using TrainerJournal.Application.Services.Students;

namespace TrainerJournal.Application.Services.BalanceChanges;

public class BalanceChangeManager(
    IStudentRepository studentRepository,
    IBalanceChangeRepository balanceChangeRepository) : IBalanceChangeManager
{
    public async Task<float> GetStudentBalanceOnDate(Guid userId, DateTime date)
    {
        var student = await studentRepository.GetByUserIdAsync(userId);
        if (student == null) return float.NaN;
        
        var balanceChange = await balanceChangeRepository.GetStudentBalanceChangeOnDateAsync(userId, date);
        
        return balanceChange?.PreviousBalance ?? student.Balance;
    }

    public async Task<(float StartBalance, float Expenses, float Payments, float EndBalance)> GetStudentBalanceReport(
        Guid userId, DateTime start, DateTime end)
    {
        var student = await studentRepository.GetByUserIdAsync(userId);
        if (student == null) return (0, 0, 0, 0);
        
        var balanceChanges = await balanceChangeRepository.GetStudentBalanceChangesAsync(
            userId, start, end);

        if (balanceChanges.Count == 0)
        {
            var edgeBalanceChange = await balanceChangeRepository.GetStudentLeftEdgeBalanceChangeAsync
                (userId, start, end);

            var balance = edgeBalanceChange?.GetAfterBalance() ?? 0;
            return (balance, 0, 0, balance);
        }

        var startBalance = balanceChanges.First().PreviousBalance;
        var endBalance = balanceChanges.Last().GetAfterBalance();

        var expenses = 0.0f;
        var payments = 0.0f;
        
        foreach (var balanceChange in balanceChanges)
        {
            if (balanceChange.IsExpense())
            {
                expenses += balanceChange.Amount;
            }
            else if (balanceChange.IsPayment())
            {
                payments += balanceChange.Amount;
            }
        }

        return (startBalance, expenses, payments, endBalance);
    }

}