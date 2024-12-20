using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.ValueObjects;

namespace TrainerJournal.Application.Services.Groups.Colors;

public interface IColorGenerator
{
    public HexColor GetRandomGroupColor();
}