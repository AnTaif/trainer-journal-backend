using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;
using TrainerJournal.Domain.Enums.ViewSchedule;

namespace TrainerJournal.Application.Services.Schedules;

public class ScheduleService(
    IGroupRepository groupRepository,
    IPracticeRepository practiceRepository,
    IScheduleRepository scheduleRepository) : IScheduleService
{
    public async Task<Result<List<ScheduleItemDto>>> GetScheduleAsync(Guid userId, DateTime Date, ViewSchedule View)
    {
        var start = Date.Date;
        var end = start + View.ToTimeSpan();
        var scheduleDays = View.ToTimeSpan().Days;

        var singlePractices = await practiceRepository.GetSinglePracticesByUserIdAsync(userId, start, end);
        var schedules = await scheduleRepository.GetAllByUserIdAsync(userId, start, end);

        var responseList = GetScheduleResponseList(singlePractices, schedules, scheduleDays, start);

        return responseList;
    }

    public async Task<Result<List<ScheduleItemDto>>> GetGroupScheduleAsync(Guid groupId, DateTime Date,
        ViewSchedule View)
    {
        var start = Date.Date;
        var end = start + View.ToTimeSpan();
        var scheduleDays = View.ToTimeSpan().Days;

        var singlePractices = await practiceRepository.GetSinglePracticesByGroupIdAsync(groupId, start, end);
        var schedules = await scheduleRepository.GetAllByGroupIdAsync(groupId, start, end);

        var responseList = GetScheduleResponseList(singlePractices, schedules, scheduleDays, start);

        return responseList;
    }

    public async Task<Result<List<ScheduleItemDto>>> CreateScheduleAsync(Guid trainerId, Guid groupId,
        CreateScheduleRequest request)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        var schedule = new Schedule(groupId, request.StartDay.Date, request.Until?.Date);
        await scheduleRepository.AddAsync(schedule);

        var schedulePractices = request.Practices
            .Select(practiceRequest =>
                new SchedulePractice(
                    schedule.Id,
                    group.Id,
                    group.Price,
                    practiceRequest.Start,
                    practiceRequest.End,
                    group.HallAddress,
                    PracticeType.Regular,
                    trainerId))
            .ToList();

        await practiceRepository.AddRangeAsync(schedulePractices);

        var oldSchedule = await scheduleRepository.GetGroupActiveScheduleAsync(group.Id);
        oldSchedule?.SetUntil(schedule.StartDay - TimeSpan.FromDays(1));

        await practiceRepository.SaveChangesAsync();
        return schedulePractices.Select(s => s.ToItemDto(group)).ToList();
    }

    private List<ScheduleItemDto> GetScheduleResponseList(
        List<SinglePractice> singlePractices,
        List<Schedule> schedules,
        int scheduleDays,
        DateTime start)
    {
        var responseList = new List<ScheduleItemDto>();
        var overridenStarts = new HashSet<DateTime>();

        AddSinglePracticesToResponseList(singlePractices, responseList, overridenStarts);
        AddSchedulesToResponseList(schedules, start, scheduleDays, responseList, overridenStarts);

        return responseList;
    }

    private static void AddSinglePracticesToResponseList(
        List<SinglePractice> singlePractices,
        List<ScheduleItemDto> responseList,
        HashSet<DateTime> overridenStarts)
    {
        foreach (var practice in singlePractices)
        {
            if (practice.OriginalStart != null) overridenStarts.Add(practice.OriginalStart.Value);
            responseList.Add(practice.ToItemDto());
        }
    }

    private static void AddSchedulesToResponseList(
        List<Schedule> schedules,
        DateTime start,
        int scheduleDays,
        List<ScheduleItemDto> responseList,
        HashSet<DateTime> overridenStarts)
    {
        foreach (var schedule in schedules)
            for (var i = 0; i < scheduleDays; i++)
            {
                var curr = start + TimeSpan.FromDays(i);
                var dayOfWeek = curr.DayOfWeek;

                foreach (var practice in schedule.Practices.Where(p => p.Start.DayOfWeek == dayOfWeek))
                {
                    var currentStart = SchedulePractice.CombineDateAndTime(curr, practice.Start);
                    if (currentStart < schedule.StartDay) continue;

                    var currentEnd = SchedulePractice.CombineDateAndTime(curr, practice.End);
                    if (currentEnd > schedule.Until) continue;

                    if (overridenStarts.Contains(currentStart)) continue;

                    responseList.Add(new ScheduleItemDto
                    {
                        Id = practice.Id,
                        Start = currentStart,
                        End = currentEnd,
                        GroupName = schedule.Group.Name,
                        HallAddress = practice.HallAddress,
                        PracticeType = PracticeType.Regular.ToPracticeTypeString(),
                        Price = practice.Price,
                        IsCanceled = false
                    });
                }
            }
    }
}