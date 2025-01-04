using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeRepository : IUnitOfWork
{
    Task<Practice?> FindByIdAsync(Guid id);
    
    Task<Practice?> FindByIdWithIncludesAsync(Guid id);
    
    Task<List<SinglePractice>> SelectSinglePracticesByUserIdAsync(Guid userId, DateTime start, DateTime end);

    Task<List<SinglePractice>> SelectSinglePracticesByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    void AddRange(List<SchedulePractice> newPractices);
    
    void Add(SinglePractice practice);

    void Remove(SinglePractice practice);
    
    Task<bool> HasOverridenSinglePracticeAsync(Guid overridenPracticeId, DateTime originalStart);
}
