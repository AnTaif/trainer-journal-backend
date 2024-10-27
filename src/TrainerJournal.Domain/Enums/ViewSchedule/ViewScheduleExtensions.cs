namespace TrainerJournal.Domain.Enums.ViewSchedule;

public static class ViewScheduleExtensions
{
    public static int ToDaysCount(this ViewSchedule viewSchedule)
    {
        return viewSchedule switch
        {
            ViewSchedule.Week => 7,
            ViewSchedule.Month => 30,
            ViewSchedule.Year => 365,
            _ => throw new ArgumentException("")
        };
    }
}