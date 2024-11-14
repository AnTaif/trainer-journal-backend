using ErrorOr;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Application.Services.Users.Dtos.Responses;

namespace TrainerJournal.Application.Services.Users;

public interface IUserService
{
    public Task<ErrorOr<GetUserInfoResponse>> GetInfoAsync(Guid id);
    
    public Task<ErrorOr<GetUserInfoResponse>> UpdateAsync(Guid id, UpdateUserRequest request);

    public string GenerateUsername(string fullName);

    public Task<ErrorOr<CreateUserResponse>> CreateAsync(CreateUserRequest request);
}