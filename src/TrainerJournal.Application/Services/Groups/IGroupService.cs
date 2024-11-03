using ErrorOr;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;

namespace TrainerJournal.Application.Services.Groups;

public interface IGroupService
{
    public Task<ErrorOr<GetGroupsResponse>> GetGroupsByTrainerIdAsync(Guid trainerId);

    public Task<ErrorOr<GroupDto>> GetGroupByIdAsync(Guid id);

    public Task<ErrorOr<GroupDto>> CreateGroup(CreateGroupRequest request, Guid trainerId);

    public Task<ErrorOr<GroupDto>> UpdateGroupInfoAsync(UpdateGroupInfoRequest infoRequest, Guid id, Guid trainerId);

    public Task<ErrorOr<Guid>> DeleteGroupAsync(Guid id, Guid trainerId);
}