namespace TrainerJournal.Domain.Enums.PracticeType;

public static class PracticeTypeExtensions
{
    public static string ToPracticeTypeString(this PracticeType practiceType)
    {
        return practiceType switch
        {
            PracticeType.Regular => "Тренировка",
            PracticeType.Seminar => "Семинар",
            PracticeType.MasterClass => "Мастер-класс",
            _ => throw new ArgumentException($"Invalid practiceType enum: {practiceType}")
        };
    }

    public static bool IsPracticeEnum(this string practiceTypeStr)
    {
        return practiceTypeStr is "тренировка" or "семинар" or "мастер-класс";
    }

    public static PracticeType ToPracticeTypeEnum(this string practiceTypeStr)
    {
        return practiceTypeStr.ToLower() switch
        {
            "тренировка" => PracticeType.Regular,
            "семинар" => PracticeType.Seminar,
            "мастер-класс" => PracticeType.MasterClass,
            _ => throw new ArgumentException($"Invalid practiceType string: {practiceTypeStr}")
        };
    }
}