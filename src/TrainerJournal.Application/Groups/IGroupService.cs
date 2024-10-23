using ErrorOr;
using TrainerJournal.Application.Groups.Dtos;
using TrainerJournal.Application.Groups.Dtos.Requests;

namespace TrainerJournal.Application.Groups;

public interface IGroupService
{
    public Task<ErrorOr<List<GroupItemDto>>> GetGroupsByTrainerIdAsync(Guid trainerId);

    public Task<ErrorOr<GroupDto>> GetGroupByIdAsync(Guid id);

    public Task<ErrorOr<GroupDto>> CreateGroup(CreateGroupRequest request, Guid trainerId);

    public Task<ErrorOr<GroupDto>> ChangeGroupAsync(ChangeGroupRequest request, Guid id, Guid trainerId);

    public Task<ErrorOr<Guid>> DeleteGroupAsync(Guid id, Guid trainerId);
}