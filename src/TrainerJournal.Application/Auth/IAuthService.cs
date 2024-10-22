using TrainerJournal.Application.Auth.Dtos.Requests;
using TrainerJournal.Application.Auth.Dtos.Responses;

namespace TrainerJournal.Application.Auth;

public interface IAuthService
{
    public Task<LoginResponse> LoginAsync(LoginRequest request);
}