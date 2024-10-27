namespace TrainerJournal.Domain.Enums.PracticeType;

public static class PracticeTypeExtensions
{
    public static string ToPracticeTypeString(this PracticeType practiceType)
    {
        return practiceType switch
        {
            PracticeType.Training => "Тренировка",
            PracticeType.Seminar => "Семинар",
            PracticeType.MasterClass => "Мастер-класс",
            _ => throw new ArgumentException($"Invalid practiceType enum: {practiceType}")
        };
    }

    public static PracticeType ToPracticeTypeEnum(this string practiceTypeStr)
    {
        return practiceTypeStr.ToLower() switch
        {
            "тренировка" => PracticeType.Training,
            "семинар" => PracticeType.Seminar,
            "мастер-класс" => PracticeType.MasterClass,
            _ => throw new ArgumentException($"Invalid practiceType string: {practiceTypeStr}")
        };
    }
}