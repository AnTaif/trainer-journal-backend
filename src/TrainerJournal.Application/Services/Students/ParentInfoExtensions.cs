using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public static class ParentInfoExtensions
{
    public static ExtraContactDto ToDto(this ExtraContact info)
    {
        return new ExtraContactDto(info.Name, info.Contact);
    }
}