using ErrorOr;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Groups;

public class GroupService(
    IGroupRepository groupRepository,
    ILogger<GroupService> logger) : IGroupService
{
    public async Task<ErrorOr<List<GroupItemDto>>> GetGroupsByTrainerIdAsync(Guid trainerId)
    {
        var groups = await groupRepository.GetAllByTrainerIdAsync(trainerId);

        return groups.Select(g => g.ToItemDto()).ToList();
    }

    //TODO: protect from other trainers/students
    public async Task<ErrorOr<GroupDto>> GetGroupByIdAsync(Guid id)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            logger.LogWarning("Group not found: {id}", id);
            return Error.NotFound("Group.GetGroupById", "Group not found");
        }
        
        return group.ToDto();
    }

    //TODO: validate hallId
    public async Task<ErrorOr<GroupDto>> CreateGroup(CreateGroupRequest request, Guid trainerId)
    {
        var newGroup = new Group(request.Name, trainerId, request.HallId);

        groupRepository.Add(newGroup);
        await groupRepository.SaveChangesAsync();

        return newGroup.ToDto();
    }

    public async Task<ErrorOr<GroupDto>> ChangeGroupAsync(ChangeGroupRequest request, Guid id, Guid trainerId)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return Error.NotFound(description: "Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden(description: "You don't have access to this group");
        
        group.Update(request.Name, request.TrainerId, request.HallId);

        return group.ToDto();
    }

    public async Task<ErrorOr<Guid>> DeleteGroupAsync(Guid id, Guid trainerId)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return Error.NotFound(description: "Group not found");
        if (group.TrainerId != trainerId) return Error.Forbidden(description: "You don't have access to this group");
        
        groupRepository.Remove(group);
        await groupRepository.SaveChangesAsync();

        return group.Id;
    }
}