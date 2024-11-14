using MediatR;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Application.Events;

public class GroupPriceChangedEventHandler(
    ILogger<GroupPriceChangedEventHandler> logger,
    IPracticeRepository practiceRepository,
    IScheduleRepository scheduleRepository) : INotificationHandler<GroupPriceChangedEvent>
{
    public async Task Handle(
        GroupPriceChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event GroupPriceChangedEvent handled");
        await HandleScheduleChangeWithNewPriceAsync(domainEvent);
    }

    private async Task HandleScheduleChangeWithNewPriceAsync(GroupPriceChangedEvent domainEvent)
    {
        var date = DateTime.UtcNow;
        
        var schedules = await scheduleRepository.GetAllByGroupIdAsync(
            domainEvent.Group.Id, date, DateTime.MaxValue);

        foreach (var schedule in schedules)
        {
            if (schedule.StartDay < date && IsActuallyStarted(schedule, date))
            {
                var newSchedule = new Schedule(domainEvent.Group.Id, date);
                schedule.SetUntil(date - TimeSpan.FromSeconds(1));

                var newPractices = schedule.Practices
                    .Select(p =>
                        new SchedulePractice(
                            newSchedule.Id, 
                            p.GroupId, 
                            domainEvent.NewPrice, 
                            CalculateDateInSchedule(p.Start, newSchedule.StartDay), 
                            CalculateDateInSchedule(p.End, newSchedule.StartDay), 
                            p.PracticeType, 
                            p.TrainerId))
                    .ToList();

                await scheduleRepository.AddAsync(newSchedule);
                await practiceRepository.AddRangeAsync(newPractices);
            }
            else
            {
                var practices = schedule.Practices;
                foreach (var practice in practices)
                {
                    practice.ChangePrice(domainEvent.NewPrice);
                }
            }
        }
    }
    
    /// <summary>
    /// Расчет даты занятия в определенном Schedule
    /// </summary>
    private static DateTime CalculateDateInSchedule(DateTime practiceDate, DateTime scheduleStart)
    {
        var diff = (scheduleStart.Date - practiceDate.Date).Days;
        if (diff <= 0) return practiceDate;

        var weeksPassed = (int)Math.Round(diff / 7.0);
        var resultDate = scheduleStart.AddDays(weeksPassed * 7);

        return resultDate.Date + practiceDate.TimeOfDay;
    }

    /// <summary>
    /// Проверка, что как минимум 1 тренировка из расписания уже была проведена
    /// </summary>
    private bool IsActuallyStarted(Schedule schedule, DateTime date)
    {
        return schedule.Practices.Any(p => p.Start < date && p.End < date);
    }
}