using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Trainers;

public interface ITrainerRepository
{
    public Task<Trainer?> GetByUserIdAsync(Guid userId);
}