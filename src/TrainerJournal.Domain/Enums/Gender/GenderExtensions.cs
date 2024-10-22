namespace TrainerJournal.Domain.Enums.Gender;

public static class GenderExtensions
{
    public static Gender ToGenderEnum(this string genderStr)
    {
        return genderStr.ToLower() switch
        {
            "м" => Gender.Male,
            "ж" => Gender.Female,
            _ => throw new ArgumentException($"Invalid gender string: {genderStr}")
        };
    }
    
    public static bool IsGenderEnum(this string genderStr)
    {
        return genderStr.ToLower() switch
        {
            "м" => true,
            "ж" => true,
            _ => false
        };
    }

    public static string ToGenderString(this Gender gender)
    {
        return gender switch
        {
            Gender.Male => "М",
            Gender.Female => "Ж",
            _ => throw new ArgumentException($"Invalid gender enum: {gender}")
        };
    }
}