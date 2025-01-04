using MediatR;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Application.Events.Handlers;

public class GroupChangedEventHandler(
    ILogger<GroupChangedEventHandler> logger,
    IPracticeRepository practiceRepository,
    IScheduleRepository scheduleRepository) : INotificationHandler<GroupChangedEvent>
{
    public async Task Handle(
        GroupChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event GroupPriceChangedEvent handled");
        await HandleScheduleChangeAsync(domainEvent);
    }

    private async Task HandleScheduleChangeAsync(GroupChangedEvent domainEvent)
    {
        var date = DateTime.UtcNow;
        
        var schedules = await scheduleRepository.SelectByGroupIdAsync(
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
                            domainEvent.NewPrice ?? p.Price, 
                            CalculateDateInSchedule(p.Start, newSchedule.StartDay), 
                            CalculateDateInSchedule(p.End, newSchedule.StartDay), 
                            domainEvent.NewHallAddress ?? "",
                            p.PracticeType, 
                            p.TrainerId))
                    .ToList();

                scheduleRepository.Add(newSchedule);
                practiceRepository.AddRange(newPractices);
            }
            else
            {
                var practices = schedule.Practices;
                foreach (var practice in practices)
                {
                    if (domainEvent.NewPrice != null) practice.ChangePrice(domainEvent.NewPrice.Value);
                    if (domainEvent.NewHallAddress != null) practice.ChangeHallAddress(domainEvent.NewHallAddress);
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