using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeRepository
{
    public Task<Practice?> GetByIdAsync(Guid id);

    public Task<List<Practice>> GetByUserIdAsync(Guid userId, DateTime startDate, int daysCount);

    public Task<List<Practice>> GetByGroupIdAsync(Guid groupId, DateTime startDate, int daysCount);
    
    public void Add(Practice practice);

    public void AddRange(IEnumerable<Practice> practices);

    public Task SaveChangesAsync();
}