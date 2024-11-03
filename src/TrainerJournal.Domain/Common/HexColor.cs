using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Common;

[ComplexType]
public class HexColor(string code)
{
    public string Code { get; init; } = code;

    public override bool Equals(object? obj) => Equals(obj as HexColor);

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    private bool Equals(HexColor? other)
    {
        return other != null && Code == other.Code;
    }

    public override string ToString()
    {
        return Code;
    }
}