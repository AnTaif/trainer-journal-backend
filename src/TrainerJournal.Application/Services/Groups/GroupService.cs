using ErrorOr;
using TrainerJournal.Application.Services.Colors;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public class GroupService(
    IGroupRepository groupRepository,
    IColorGenerator colorGenerator) : IGroupService
{
    public async Task<ErrorOr<GetGroupsResponse>> GetGroupsByTrainerIdAsync(Guid userId)
    {
        var groups = await groupRepository.GetAllByUserIdAsync(userId);

        return new GetGroupsResponse(
            groups.Sum(g => g.Students.Count), 
            groups.Select(g => g.ToItemDto()).ToList());
    }

    //TODO: protect from other trainers/students
    public async Task<ErrorOr<GroupDto>> GetGroupByIdAsync(Guid id)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return Error.NotFound(description: "Group not found");
        
        return group.ToDto();
    }
    
    public async Task<ErrorOr<GroupDto>> CreateGroup(CreateGroupRequest request, Guid trainerId)
    {
        var newGroup = new Group(
            request.Name, 
            request.HexColor == null ? colorGenerator.GetRandomGroupColor() : new HexColor(request.HexColor), 
            trainerId);

        groupRepository.Add(newGroup);
        await groupRepository.SaveChangesAsync();

        return newGroup.ToDto();
    }

    public async Task<ErrorOr<GroupDto>> UpdateGroupInfoAsync(UpdateGroupInfoRequest infoRequest, Guid id, Guid trainerId)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return Error.NotFound(description: "Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden(description: "You don't have access to this group");
        
        group.UpdateInfo(infoRequest.Name, infoRequest.HexColor);
        await groupRepository.SaveChangesAsync();

        return group.ToDto();
    }

    public async Task<ErrorOr<Guid>> DeleteGroupAsync(Guid id, Guid trainerId)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return Error.NotFound(description: "Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden(description: "You don't have access to this group");

        group.Delete();
        await groupRepository.SaveChangesAsync();

        return group.Id;
    }
}