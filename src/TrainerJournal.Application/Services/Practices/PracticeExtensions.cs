using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public static class PracticeExtensions
{
    public static PracticeInfoDto ToInfoDto(this Practice practice)
    {
        return new PracticeInfoDto(practice.Id, practice.StartDate, practice.EndDate, practice.IsCanceled,
            practice.CancelComment, practice.PracticeType.ToPracticeTypeString(), practice.Price,
            new PracticeTrainerInfoDto(practice.Trainer.Id, practice.Trainer.User.FullName.ToString()),
            new PracticeGroupInfoDto(practice.GroupId, practice.Group.Name),
            new PracticeHallInfoDto(practice.HallId, practice.Hall.Location));
    }
}