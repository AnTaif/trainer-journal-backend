using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Groups.Colors;

public interface IColorGenerator
{
    public HexColor GetRandomGroupColor();
}