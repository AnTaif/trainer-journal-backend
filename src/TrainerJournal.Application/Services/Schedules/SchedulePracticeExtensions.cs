using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Schedules;

public static class SchedulePracticeExtensions
{
    public static ScheduleItemDto ToItemDto(this SchedulePractice practice, Group group)
    {
        return new ScheduleItemDto(practice.Id, practice.Start, practice.End, group.Name,
            practice.PracticeType.ToPracticeTypeString(), practice.Price, false);
    }
}