using TrainerJournal.Application.Services.BalanceChanges.Dtos;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.BalanceChanges;

public class BalanceChangeService(
    IStudentRepository studentRepository,
    IBalanceChangeRepository balanceChangeRepository,
    IBalanceChangeManager balanceChangeManager) : IBalanceChangeService
{
    public async Task<Result<List<BalanceChangeDto>>> GetStudentBalanceChanges(string username, DateTime start, DateTime end)
    {
        var student = await studentRepository.GetByUsernameAsync(username);
        if (student == null) return Error.NotFound("Student not found");

        var changes = await balanceChangeRepository.GetStudentBalanceChangesAsync(student.Id, start, end);

        return changes.Select(c => c.ToDto(username)).ToList();
    }

    public async Task<Result<BalanceChangeReportDto>> GetStudentBalanceChangeReportAsync(string username, DateTime start, DateTime end)
    {
        var student = await studentRepository.GetByUsernameAsync(username);
        if (student == null) return Error.NotFound("Student not found");

        var report = await balanceChangeManager.GetStudentBalanceReport(student.Id, start, end);

        return new BalanceChangeReportDto
        {
            StartBalance = report.StartBalance,
            Expenses = report.Expenses,
            Payments = report.Payments,
            EndBalance = report.EndBalance
        };
    }
}