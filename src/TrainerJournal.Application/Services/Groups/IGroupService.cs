using ErrorOr;
using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;

namespace TrainerJournal.Application.Services.Groups;

public interface IGroupService
{
    public Task<ErrorOr<GetGroupsResponse>> GetGroupsByTrainerIdAsync(Guid userId);

    public Task<ErrorOr<GroupDto>> GetByIdAsync(Guid id);

    public Task<ErrorOr<GroupDto>> CreateAsync(CreateGroupRequest request, Guid trainerId);

    public Task<ErrorOr<GroupDto>> UpdateInfoAsync(UpdateGroupInfoRequest request, Guid id, Guid trainerId);

    public Task<ErrorOr<Guid>> DeleteAsync(Guid id, Guid trainerId);
}