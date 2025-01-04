using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Trainers;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class TrainerRepository(AppDbContext context) : BaseRepository(context), ITrainerRepository
{
    private DbSet<Trainer> trainers => dbContext.Trainers;
    
    public async Task<Trainer?> GetByUserIdAsync(Guid userId)
    {
        return await trainers
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == userId);
    }

    public async Task AddAsync(Trainer trainer)
    {
        await trainers.AddAsync(trainer);
    }
}