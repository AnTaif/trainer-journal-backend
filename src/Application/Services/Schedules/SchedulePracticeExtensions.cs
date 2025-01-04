using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Schedules;

public static class SchedulePracticeExtensions
{
    public static ScheduleItemDto ToItemDto(this SchedulePractice practice, Group group)
    {
        return new ScheduleItemDto
        {
            Id = practice.Id,
            Start = practice.Start,
            End = practice.End,
            GroupName = group.Name,
            HallAddress = practice.HallAddress,
            PracticeType = practice.PracticeType.ToPracticeTypeString(),
            Price = practice.Price,
            IsCanceled = false
        };
    }

    public static ScheduleItemDto ToItemDto(this SinglePractice practice)
    {
        return new ScheduleItemDto
        {
            Id = practice.Id,
            Start = practice.Start,
            End = practice.End,
            GroupName = practice.GroupId != null ? practice.Group.Name : null,
            HallAddress = practice.HallAddress,
            PracticeType = practice.PracticeType.ToPracticeTypeString(),
            Price = practice.Price,
            IsCanceled = practice.IsCanceled
        };
    }
}