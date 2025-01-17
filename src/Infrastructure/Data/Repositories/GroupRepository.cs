using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class GroupRepository(AppDbContext context) : BaseRepository(context), IGroupRepository
{
    private DbSet<Group> groups => dbContext.Groups;
    
    public async Task<Group?> FindByIdAsync(Guid id)
    {
        return await groups
            .Include(g => g.Students)
            .ThenInclude(s => s.User)
            .Include(g => g.Trainer)
            .ThenInclude(t => t.User)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
    
    public async Task<List<Group>> SelectByUserIdAsync(Guid userId)
    {
        return await groups
            .Include(g => g.Students)
            .Include(g => g.Trainer)
            .Where(g => !g.IsDeleted 
                        && (g.TrainerId == userId || g.Students.Any(s => s.Id == userId)))
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<List<Group>> SelectByStudentUsernameAsync(string username)
    {
        return await groups
            .Include(g => g.Students)
                .ThenInclude(s => s.User)
            .Include(g => g.Trainer)
            .Where(g => !g.IsDeleted && g.Students.Any(s => s.User.UserName == username))
            .OrderBy(g => g.Name)
            .ToListAsync();
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
}