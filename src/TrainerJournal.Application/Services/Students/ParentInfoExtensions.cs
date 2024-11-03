using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Students;

public static class ParentInfoExtensions
{
    public static ParentInfo ToInfo(this ParentInfoDto dto)
    {
        return new ParentInfo(dto.Name, dto.Contact);
    }

    public static ParentInfoDto ToDto(this ParentInfo info)
    {
        return new ParentInfoDto(info.Name, info.Contact);
    }
}