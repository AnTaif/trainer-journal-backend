using TrainerJournal.Application.Services.Users.Dtos;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Users;

public interface IUserService
{
    public Task<Result<FullInfoDto>> GetMyInfoAsync(Guid id);

    public Task<Result<FullInfoDto>> GetInfoByUsernameAsync(Guid requestedUserId, string username);

    public Task<Result<FullInfoWithoutCredentialsDto>> UpdateMyInfoAsync(Guid id, UpdateFullInfoRequest request);

    public Task<Result<FullInfoWithoutCredentialsDto>> UpdateStudentInfoAsync(Guid userId, string username,
        UpdateUserStudentInfoRequest request);

    public string GenerateUsername(string fullName);

    public Task<Result<User>> CreateAsync(string fullName, Gender gender);
}