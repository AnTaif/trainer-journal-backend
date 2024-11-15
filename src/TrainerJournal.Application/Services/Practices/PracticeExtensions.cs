using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public static class PracticeExtensions
{
    public static PracticeDto ToDto(this Practice practice, DateTime time)
    {
        return practice switch
        {
            SinglePractice singlePractice => singlePractice.ToDto(),
            SchedulePractice schedulePractice => schedulePractice.ToDto(time),
            _ => throw new Exception("Practice type is unrecognized")
        };
    }
    
    public static PracticeDto ToDto(this SinglePractice singlePractice)
    {
        return new PracticeDto
        {
            Id = singlePractice.Id,
            Start = singlePractice.Start,
            End = singlePractice.End,
            Group = new PracticeGroupDto
            {
                Id = singlePractice.GroupId,
                Name = singlePractice.Group.Name
            },
            Trainer = new PracticeTrainerDto
            {
                Id = singlePractice.TrainerId,
                FullName = singlePractice.Trainer.User.FullName.ToString()
            },
            PracticeType = singlePractice.PracticeType.ToPracticeTypeString(),
            HallAddress = singlePractice.HallAddress,
            Price = singlePractice.Price,
            IsCanceled = singlePractice.IsCanceled,
            CancelComment = singlePractice.CancelComment
        };
    }

    public static PracticeDto ToDto(this SchedulePractice schedulePractice, DateTime date)
    {
        var start = SchedulePractice.CombineDateAndTime(date, schedulePractice.Start);
        var end = SchedulePractice.CombineDateAndTime(date, schedulePractice.End);
        
        return new PracticeDto
        {
            Id = schedulePractice.Id,
            Start = start,
            End = end,
            Group = new PracticeGroupDto
            {
                Id = schedulePractice.GroupId,
                Name = schedulePractice.Group.Name
            },
            Trainer = new PracticeTrainerDto
            {
                Id = schedulePractice.TrainerId,
                FullName = schedulePractice.Trainer.User.FullName.ToString()
            },
            PracticeType = schedulePractice.PracticeType.ToPracticeTypeString(),
            HallAddress = schedulePractice.HallAddress,
            Price = schedulePractice.Price,
            IsCanceled = false,
            CancelComment = null
        };
    }
}