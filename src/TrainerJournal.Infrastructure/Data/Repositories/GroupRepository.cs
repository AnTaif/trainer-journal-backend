using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class GroupRepository(AppDbContext dbContext) : IGroupRepository
{
    private DbSet<Group> groups => dbContext.Groups;
    
    public async Task<List<Group>> GetAllByUserIdAsync(Guid userId)
    {
        return await groups
            .Include(g => g.Students)
            .Include(g => g.Trainer)
            .Where(g => !g.IsDeleted 
                        && (g.TrainerId == userId || g.Students.Any(s => s.UserId == userId)))
            .ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await groups
            .Include(g => g.Students)
            .Include(g => g.Trainer)
            .FirstOrDefaultAsync(g => g.Id == id);
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