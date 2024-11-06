using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class TrainerRepository(AppDbContext dbContext) : ITrainerRepository
{
    private DbSet<Trainer> trainers => dbContext.Trainers;
    
    public async Task<Trainer?> GetByUserIdAsync(Guid userId)
    {
        return await trainers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }
}