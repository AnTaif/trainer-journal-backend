using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public static class PracticeExtensions
{
    public static PracticeDto ToDto(this SinglePractice singlePractice)
    {
        return new PracticeDto(singlePractice.Id, singlePractice.Start, singlePractice.End,
            new PracticeGroupDto(singlePractice.GroupId, singlePractice.Group.Name),
            new PracticeTrainerDto(singlePractice.TrainerId, singlePractice.Trainer.User.FullName.ToString()),
            singlePractice.PracticeType.ToPracticeTypeString(), singlePractice.Price, 
            singlePractice.IsCanceled, singlePractice.CancelComment);
    }

    public static PracticeDto ToDto(this SchedulePractice schedulePractice, DateTime date)
    {
        var start = SchedulePractice.CombineDateAndTime(date, schedulePractice.Start);
        var end = SchedulePractice.CombineDateAndTime(date, schedulePractice.End);
        
        return new PracticeDto(schedulePractice.Id, start, end,
            new PracticeGroupDto(schedulePractice.GroupId, schedulePractice.Group.Name),
            new PracticeTrainerDto(schedulePractice.TrainerId, schedulePractice.Trainer.User.FullName.ToString()),
            schedulePractice.PracticeType.ToPracticeTypeString(), schedulePractice.Price, 
            false, null);
    }
}