using TrainerJournal.Application.Services.Auth.Dtos.Requests;
using TrainerJournal.Application.Services.Auth.Dtos.Responses;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Auth;

public interface IAuthService
{
    public Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
}