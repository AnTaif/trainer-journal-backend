using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public static class GroupExtensions
{
    public static GroupDto ToDto(this Group group)
    {
        return new GroupDto
        {
            Id = group.Id,
            Name = group.Name,
            StudentsCount = group.Students.Count,
            TrainerId = group.TrainerId,
            HallAddress = group.HallAddress,
            HexColor = group.HexColor.ToString(),
            Price = group.Price
        };
    }
    
    public static GroupItemDto ToItemDto(this Group group)
    {
        return new GroupItemDto
        {
            Id = group.Id,
            Name = group.Name,
            HallAddress = group.HallAddress,
            HexColor = group.HexColor.ToString(),
            StudentsCount = group.Students.Count,
            Price = group.Price
        };
    }
}