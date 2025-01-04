using System.Text;

namespace TrainerJournal.Application.Services.Users;

public static class CyrillicTextConverter
{
    private static readonly Dictionary<char, string> convertedLetters = new()
    {
        {'а', "a"},
        {'б', "b"},
        {'в', "v"},
        {'г', "g"},
        {'д', "d"},
        {'е', "e"},
        {'ё', "yo"},
        {'ж', "zh"},
        {'з', "z"},
        {'и', "i"},
        {'й', "j"},
        {'к', "k"},
        {'л', "l"},
        {'м', "m"},
        {'н', "n"},
        {'о', "o"},
        {'п', "p"},
        {'р', "r"},
        {'с', "s"},
        {'т', "t"},
        {'у', "u"},
        {'ф', "f"},
        {'х', "kh"},
        {'ц', "c"},
        {'ч', "ch"},
        {'ш', "sh"},
        {'щ', "sch"},
        {'ъ', ""},
        {'ы', "i"},
        {'ь', ""},
        {'э', "e"},
        {'ю', "yu"},
        {'я', "ya"},
        {'А', "A"},
        {'Б', "B"},
        {'В', "V"},
        {'Г', "G"},
        {'Д', "D"},
        {'Е', "E"},
        {'Ё', "Yo"},
        {'Ж', "Zh"},
        {'З', "Z"},
        {'И', "I"},
        {'Й', "J"},
        {'К', "K"},
        {'Л', "L"},
        {'М', "M"},
        {'Н', "N"},
        {'О', "O"},
        {'П', "P"},
        {'Р', "R"},
        {'С', "S"},
        {'Т', "T"},
        {'У', "U"},
        {'Ф', "F"},
        {'Х', "Kh"},
        {'Ц', "C"},
        {'Ч', "Ch"},
        {'Ш', "Sh"},
        {'Щ', "Sch"},
        {'Ъ', ""},
        {'Ы', "I"},
        {'Ь', ""},
        {'Э', "E"},
        {'Ю', "Yu"},
        {'Я', "Ya"},
        {' ', " "}
    };

    public static string ConvertToLatin(string source)
    {
        var result = new StringBuilder();
        foreach (var letter in source)
        {
            if (IsLatin(letter))
            {
                result.Append(letter);
                continue;
            }
            
            if (convertedLetters.TryGetValue(letter, out var convertedLetter))
            {
                result.Append(convertedLetter);
            }
        }
        return result.ToString();
    }
    
    private static bool IsLatin(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
    }
}