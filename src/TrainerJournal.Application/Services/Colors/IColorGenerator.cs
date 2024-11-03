using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Colors;

public interface IColorGenerator
{
    public HexColor GetRandomGroupColor();
}