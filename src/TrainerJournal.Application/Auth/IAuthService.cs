using ErrorOr;
using TrainerJournal.Application.Auth.Dtos.Requests;
using TrainerJournal.Application.Auth.Dtos.Responses;

namespace TrainerJournal.Application.Auth;

public interface IAuthService
{
    public Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest request);
}