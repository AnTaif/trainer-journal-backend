using ErrorOr;
using TrainerJournal.Application.Users.Dtos.Responses;

namespace TrainerJournal.Application.Users;

public interface IUserService
{
    public Task<ErrorOr<GetUserInfoResponse>> GetInfoAsync(Guid id);
}