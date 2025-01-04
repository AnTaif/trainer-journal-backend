using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.BalanceChanges;

public class BalanceChangeManager(
    IStudentRepository studentRepository,
    IBalanceChangeRepository balanceChangeRepository) : IBalanceChangeManager
{
    public async Task<float> GetStudentBalanceOnDate(Guid userId, DateTime date)
    {
        var student = await studentRepository.FindByUserIdAsync(userId);
        if (student == null) return float.NaN;
        
        var balanceChange = await balanceChangeRepository.FindOnDateAsync(userId, date);
        
        return balanceChange?.PreviousBalance ?? student.Balance;
    }

    public async Task<(float StartBalance, float Expenses, float Payments, float EndBalance)> GetStudentBalanceReport(
        Guid userId, DateTime start, DateTime end)
    {
        var balanceChanges = await balanceChangeRepository.SelectByStudentIdAsync(
            userId, start, end);

        if (balanceChanges.Count == 0)
        {
            // Находим последнее изменение баланса, чтобы получить инфу о балансе на период
            var lastBalanceChange = await balanceChangeRepository.FindLastByStudentIdAsync(userId, start);

            var balance = lastBalanceChange?.GetAfterBalance() ?? 0;
            
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

    public Task ChangeBalanceAsync(Student student, float balanceChange, BalanceChangeReason reason)
    {
        var date = DateTime.UtcNow;
        
        var newBalanceChange = new BalanceChange(student.Id, balanceChange, student.Balance,
            reason, date);

        student.UpdateBalance(balanceChange);
        
        balanceChangeRepository.Add(newBalanceChange);
        return Task.CompletedTask;
    }
}