using TrainerJournal.Application.Services.Groups.Dtos;
using TrainerJournal.Application.Services.Groups.Dtos.Requests;
using TrainerJournal.Application.Services.Groups.Dtos.Responses;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Groups;

public interface IGroupService
{
    public Task<Result<GetGroupsResponse>> GetGroupsByTrainerIdAsync(Guid userId);

    public Task<Result<List<GroupDto>>> GetGroupsByStudentUsernameAsync(string username);

    public Task<Result<GroupDto>> GetByIdAsync(Guid id);

    public Task<Result<GroupDto>> CreateAsync(CreateGroupRequest request, Guid trainerId);

    public Task<Result<GroupDto>> UpdateInfoAsync(UpdateGroupInfoRequest request, Guid id, Guid trainerId);

    public Task<Result> DeleteAsync(Guid id, Guid trainerId);
}