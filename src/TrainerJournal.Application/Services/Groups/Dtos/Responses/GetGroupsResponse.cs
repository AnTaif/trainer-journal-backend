namespace TrainerJournal.Application.Services.Groups.Dtos.Responses;

public class GetGroupsResponse(int studentsCount, List<GroupItemDto> groups)
{
    public int StudentsCount { get; init; } = studentsCount;
    public List<GroupItemDto> Groups { get; init; } = groups;
}