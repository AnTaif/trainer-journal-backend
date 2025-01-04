namespace TrainerJournal.Application.Services.Groups.Dtos.Responses;

public class GetGroupsResponse
{
    public int StudentsCount { get; init; }
    
    public List<GroupItemDto> Groups { get; init; }
}