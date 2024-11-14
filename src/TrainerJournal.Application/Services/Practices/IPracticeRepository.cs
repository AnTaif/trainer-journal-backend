using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeRepository
{
    public Task<Practice?> GetByIdAsync(Guid id);

    public Task<bool> HasOverridenSinglePracticeAsync(Guid overridenPracticeId, DateTime originalStart);

    public Task<List<SinglePractice>> GetSinglePracticesByUserIdAsync(Guid userId, DateTime start, DateTime end);

    public Task AddRangeAsync(List<SchedulePractice> newPractices);
    
    public Task AddAsync(SinglePractice practice);

    public Task SaveChangesAsync();
}