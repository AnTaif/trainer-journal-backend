using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public static class GroupExtensions
{
    public static GroupDto ToDto(this Group group)
    {
        return new GroupDto(group.Id, group.Name, group.TrainerId, group.HallId);
    }

    public static GroupItemDto ToItemDto(this Group group)
    {
        return new GroupItemDto(group.Id, group.Name, group.TrainerId, group.HallId);
    }
}