using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Options;
using TrainerJournal.Domain.ValueObjects;

namespace TrainerJournal.Application.Services.Groups.Colors;

public class ColorGenerator : IColorGenerator
{
    private static readonly Random rnd = new();
    
    public HexColor GetRandomGroupColor()
    {
        var groupColor = GroupColorOptions.HexColors;
        var i = rnd.Next(groupColor.Length);
        return new HexColor(groupColor[i]);
    }
}