using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Trainers;

public interface ITrainerRepository
{
    public Task<Trainer?> GetByUserIdAsync(Guid userId);

    Task AddAsync(Trainer trainer);

    Task SaveChangesAsync();
}