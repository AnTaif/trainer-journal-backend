using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Groups;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class GroupRepository(AppDbContext dbContext) : IGroupRepository
{
    private DbSet<Group> groups => dbContext.Groups;
    
    public async Task<List<Group>> GetAllByTrainerIdAsync(Guid trainerId)
    {
        return await groups.Where(g => g.TrainerId == trainerId).ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await groups.FindAsync(id);
    }

    public void Add(Group group)
    {
        groups.Add(group);
    }

    public void Remove(Group group)
    {
        groups.Remove(group);
    }

    public void Update(Group group)
    {
        groups.Update(group);
    }

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
}