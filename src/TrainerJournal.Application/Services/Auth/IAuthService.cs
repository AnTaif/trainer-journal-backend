using ErrorOr;
using TrainerJournal.Application.Services.Auth.Dtos.Requests;
using TrainerJournal.Application.Services.Auth.Dtos.Responses;

namespace TrainerJournal.Application.Services.Auth;

public interface IAuthService
{
    public Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest request);
}