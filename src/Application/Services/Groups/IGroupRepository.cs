using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public interface IGroupRepository : IUnitOfWork
{
    Task<Group?> FindByIdAsync(Guid id);
    
    Task<List<Group>> SelectByUserIdAsync(Guid userId);

    Task<List<Group>> SelectByStudentUsernameAsync(string username);

    void Add(Group group);

    void Remove(Group group);

    void Update(Group group);
}