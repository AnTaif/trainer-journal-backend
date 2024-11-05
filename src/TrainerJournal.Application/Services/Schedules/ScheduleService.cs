using ErrorOr;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;
using TrainerJournal.Domain.Enums.ViewSchedule;

namespace TrainerJournal.Application.Services.Schedules;

public class ScheduleService(
    IGroupRepository groupRepository,
    IPracticeRepository practiceRepository,
    IScheduleRepository scheduleRepository) : IScheduleService
{
        public async Task<ErrorOr<List<ScheduleItemDto>>> GetScheduleAsync(Guid userId, DateTime Date, ViewSchedule View)
    {
        var start = Date.Date;
        var end = start + View.ToTimeSpan();
        var scheduleDays = View.ToTimeSpan().Days;

        var responseList = new List<ScheduleItemDto>();
        var overridenStarts = new HashSet<DateTime>();

        await AddSinglePracticesToResponseList(userId, start, end, responseList, overridenStarts);
        await AddSchedulesToResponseList(userId, start, end, scheduleDays, responseList, overridenStarts);

        return responseList;
    }

    private async Task AddSinglePracticesToResponseList(
        Guid userId, 
        DateTime start, 
        DateTime end, 
        List<ScheduleItemDto> responseList, 
        HashSet<DateTime> overridenStarts)
    {
        var singlePractices = await practiceRepository.GetSinglePracticesByUserIdAsync(userId, start, end);
        foreach (var practice in singlePractices)
        {
            if (practice.OriginalStart != null) overridenStarts.Add(practice.OriginalStart.Value);

            responseList.Add(new ScheduleItemDto(
                practice.Id,
                practice.Start,
                practice.End,
                practice.Group.Name,
                practice.PracticeType.ToPracticeTypeString(),
                practice.Price,
                practice.IsCanceled));
        }
    }

    private async Task AddSchedulesToResponseList(
        Guid userId, 
        DateTime start, 
        DateTime end, 
        int scheduleDays, 
        List<ScheduleItemDto> responseList, 
        HashSet<DateTime> overridenStarts)
    {
        var schedules = await scheduleRepository.GetAllByUserIdAsync(userId, start, end);
        foreach (var schedule in schedules)
        {
            for (var i = 0; i < scheduleDays; i++)
            {
                var curr = start + TimeSpan.FromDays(i);
                var dayOfWeek = curr.DayOfWeek;

                foreach (var practice in schedule.Practices.Where(p => p.Start.DayOfWeek == dayOfWeek))
                {
                    var currentStart = CombineDateAndTime(curr, practice.Start);
                    if (currentStart < schedule.StartDay) continue;
                    
                    var currentEnd = CombineDateAndTime(curr, practice.End);
                    if (currentEnd > schedule.Until) continue;

                    if (overridenStarts.Contains(currentStart)) continue;

                    responseList.Add(new ScheduleItemDto(practice.Id, currentStart, currentEnd, schedule.Group.Name,
                        PracticeType.Regular.ToPracticeTypeString(), practice.Price, false));
                }
            }
        }
    }

    private static DateTime CombineDateAndTime(DateTime datePart, DateTime timePart)
    {
        return datePart.Date + timePart.TimeOfDay;
    }

    public async Task<ErrorOr<List<ScheduleItemDto>>> CreateScheduleAsync(Guid trainerId, CreateScheduleRequest request)
    {
        var group = await groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) return Error.NotFound(description: "Group not found");

        var schedule = new Schedule(request.GroupId, request.StartDay.Date, request.Until?.Date);
        await scheduleRepository.AddAsync(schedule);
        
        var schedulePractices = request.Practices
            .Select(practiceRequest => 
                new SchedulePractice(
                    schedule.Id,
                    group.Id, 
                    group.Price, 
                    practiceRequest.Start, 
                    practiceRequest.End, 
                    PracticeType.Regular, 
                    trainerId))
            .ToList();
        
        await practiceRepository.AddRangeAsync(schedulePractices);

        var oldSchedule = await scheduleRepository.GetGroupActiveScheduleAsync(group.Id);
        oldSchedule?.SetUntil(schedule.StartDay - TimeSpan.FromDays(1));
        
        await practiceRepository.SaveChangesAsync();
        return schedulePractices.Select(s => s.ToItemDto(group)).ToList();
    }
}