using TrainerJournal.Application.Services.Trainers.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Trainers;

public static class TrainerExtensions
{
    public static TrainerInfoDto ToInfoDto(this Trainer trainer)
    {
        return new TrainerInfoDto
        {
            Phone = trainer.Phone,
            Email = trainer.Email
        };
    }
}