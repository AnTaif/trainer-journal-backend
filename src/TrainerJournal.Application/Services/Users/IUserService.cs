using ErrorOr;
using TrainerJournal.Application.Services.Users.Dtos;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Users;

public interface IUserService
{
    public Task<ErrorOr<FullInfoDto>> GetMyInfoAsync(Guid id);

    public Task<ErrorOr<FullInfoDto>> GetInfoByUsernameAsync(Guid requestedUserId, string username);
    
    public Task<ErrorOr<FullInfoWithoutCredentialsDto>> UpdateMyInfoAsync(Guid id, UpdateFullInfoRequest request);

    public Task<ErrorOr<FullInfoWithoutCredentialsDto>> UpdateStudentInfoAsync(Guid userId, string username,
        UpdateUserStudentInfoRequest request);

    public string GenerateUsername(string fullName);

    public Task<ErrorOr<User>> CreateAsync(string fullName, Gender gender);
}