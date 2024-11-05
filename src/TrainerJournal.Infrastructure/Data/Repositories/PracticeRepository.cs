using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class PracticeRepository(AppDbContext dbContext) : IPracticeRepository
{
    private readonly DbSet<Practice> practices = dbContext.Practices;
    private readonly DbSet<SchedulePractice> schedulePractices = dbContext.SchedulePractices;
    private readonly DbSet<SinglePractice> singlePractices = dbContext.SinglePractices;
    
    public async Task<Practice?> GetByIdAsync(Guid id)
    {
        return await practices.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<SinglePractice>> GetSinglePracticesByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await singlePractices
            .Include(p => p.Group)
                .ThenInclude(g => g.Students)
            .Where(p => p.TrainerId == userId || p.Group.Students.Any(s => s.Id == userId))
            .ToListAsync();
    }

    public async Task AddRangeAsync(List<SchedulePractice> newPractices)
    {
        await schedulePractices.AddRangeAsync(newPractices);
    }

    public async Task AddAsync(SinglePractice practice)
    {
        await singlePractices.AddAsync(practice);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}