using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public interface IGroupRepository
{
    public Task<List<Group>> GetAllByUserIdAsync(Guid userId);

    public Task<Group?> GetByIdAsync(Guid id);

    public void Add(Group group);

    public void Remove(Group group);

    public void Update(Group group);

    public Task SaveChangesAsync();
}