using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Application.Services.Colors;

public class ColorGenerator : IColorGenerator
{
    private static Random rnd = new();
    
    public HexColor GetRandomGroupColor()
    {
        var groupColor = GroupColorOptions.HexColors;
        var i = rnd.Next(groupColor.Length);
        return new HexColor(groupColor[i]);
    }
}