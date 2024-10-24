using ErrorOr;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;

namespace TrainerJournal.Application.Services.Users;

public interface IUserService
{
    public Task<ErrorOr<GetUserInfoResponse>> GetInfoAsync(Guid id);

    public Task<ErrorOr<CreateUserResponse>> CreateAsync(CreateUserRequest request);
}