using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Trainers;

public interface ITrainerRepository
{
    public Task<Trainer?> GetByUserIdAsync(Guid userId);
}