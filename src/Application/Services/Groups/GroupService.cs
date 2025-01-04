using TrainerJournal.Application.Services.Groups.Colors;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Common.Result;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.ValueObjects;

namespace TrainerJournal.Application.Services.Groups;

public class GroupService(
    IGroupRepository groupRepository,
    IColorGenerator colorGenerator) : IGroupService
{
    public async Task<Result<GetGroupsResponse>> GetGroupsByTrainerIdAsync(Guid userId)
    {
        var groups = await groupRepository.SelectByUserIdAsync(userId);

        return new GetGroupsResponse
        {
            StudentsCount = groups.Sum(g => g.Students.Count),
            Groups = groups.Select(g => g.ToItemDto()).ToList()
        };
    }

    public async Task<Result<List<GroupDto>>> GetGroupsByStudentUsernameAsync(string username)
    {
        var groups = await groupRepository.SelectByStudentUsernameAsync(username);

        return groups.Select(g => g.ToDto()).ToList();
    }

    public async Task<Result<GroupDto>> GetByIdAsync(Guid id)
    {
        var group = await groupRepository.FindByIdAsync(id);
        if (group == null) return Error.NotFound("Group not found");

        return group.ToDto();
    }

    public async Task<Result<GroupDto>> CreateAsync(CreateGroupRequest request, Guid trainerId)
    {
        var newGroup = new Group(
            request.Name,
            request.Price,
            request.HallAddress ?? "",
            request.HexColor == null ? colorGenerator.GetRandomGroupColor() : new HexColor(request.HexColor),
            trainerId);

        groupRepository.Add(newGroup);
        await groupRepository.SaveChangesAsync();

        return newGroup.ToDto();
    }

    public async Task<Result<GroupDto>> UpdateInfoAsync(UpdateGroupInfoRequest request, Guid id, Guid trainerId)
    {
        var group = await groupRepository.FindByIdAsync(id);
        if (group == null) return Error.NotFound("Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden("You don't have access to this group");

        group.UpdateInfo(request.Name, request.Price, request.HallAddress, request.HexColor);
        await groupRepository.SaveChangesAsync();

        return group.ToDto();
    }

    public async Task<Result> DeleteAsync(Guid id, Guid trainerId)
    {
        var group = await groupRepository.FindByIdAsync(id);
        if (group == null) return Error.NotFound("Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden("You don't have access to this group");

        group.Delete();
        await groupRepository.SaveChangesAsync();

        return Result.Success();
    }
}