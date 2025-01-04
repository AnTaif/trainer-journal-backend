using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Trainers;

public interface ITrainerRepository : IUnitOfWork
{
    public Task<Trainer?> FindByUserIdAsync(Guid userId);

    void Add(Trainer trainer);
}