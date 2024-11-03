namespace TrainerJournal.Application.Services.Groups.Dtos.Responses;

public record GetGroupsResponse(
    int StudentsCount,
    List<GroupItemDto> Groups);