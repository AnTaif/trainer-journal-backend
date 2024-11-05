namespace TrainerJournal.Domain.Enums.ViewSchedule;

public static class ViewScheduleExtensions
{
    public static TimeSpan ToTimeSpan(this ViewSchedule viewSchedule)
    {
        return TimeSpan.FromDays(viewSchedule switch
        {
            ViewSchedule.Week => 7,
            ViewSchedule.Month => 30,
            ViewSchedule.Year => 365,
            _ => throw new ArgumentException("")
        });
    }
}