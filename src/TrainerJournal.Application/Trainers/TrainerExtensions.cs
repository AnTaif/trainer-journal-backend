using TrainerJournal.Application.Trainers.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Trainers;

public static class TrainerExtensions
{
    public static TrainerInfoDto ToInfoDto(this Trainer trainer)
    {
        return new TrainerInfoDto(trainer.Id);
    }
}