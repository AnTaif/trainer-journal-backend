namespace TrainerJournal.Domain.Enums.BalanceChangeReason;

public static class BalanceChangeExtensions
{
    public static string ToBalanceChangeString(this BalanceChangeReason reason)
    {
        return reason switch
        {
            BalanceChangeReason.Payment => "Оплата",
            BalanceChangeReason.PaymentRejection => "Отмена оплаты",
            BalanceChangeReason.MarkAttendance => "Посещение занятия",
            BalanceChangeReason.UnmarkAttendance => "Отмена посещения занятия",
            _ => throw new ArgumentException($"Invalid balance change reason enum: {reason}")
        };
    }

    public static BalanceChangeReason ToBalanceChangeEnum(this string reasonStr)
    {
        return reasonStr.ToLower() switch
        {
            "оплата" => BalanceChangeReason.Payment,
            "отмена оплаты" => BalanceChangeReason.PaymentRejection,
            "посещение занятия" => BalanceChangeReason.MarkAttendance,
            "отмена посещения занятия" => BalanceChangeReason.UnmarkAttendance,
            _ => throw new ArgumentException($"Invalid balance change reason string: {reasonStr}")
        };
    }

    public static bool IsBalanceChangeEnum(this string reasonStr)
    {
        return reasonStr.ToLower() switch
        {
            "оплата" => true,
            "отмена оплаты" => true,
            "посещение занятия" => true,
            "отмена посещения занятия" => true,
            _ => false
        };
    }
}