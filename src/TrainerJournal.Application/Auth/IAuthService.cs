using TrainerJournal.Application.Auth.Requests;
using TrainerJournal.Application.Auth.Responses;

namespace TrainerJournal.Application.Auth;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(LoginRequest request);
}