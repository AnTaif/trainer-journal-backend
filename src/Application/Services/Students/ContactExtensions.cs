using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public static class ContactExtensions
{
    public static ContactDto ToDto(this Contact info)
    {
        return new ContactDto
        {
            Name = info.Name,
            Relation = info.Relation,
            Phone = info.Phone
        };
    }

    public static Contact ToEntity(this ContactDto dto)
    {
        return new Contact(dto.Name, dto.Relation, dto.Phone);
    }
}