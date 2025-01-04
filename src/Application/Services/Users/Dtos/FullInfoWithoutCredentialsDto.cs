using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Trainers.Dtos;

namespace TrainerJournal.Application.Services.Users.Dtos;

public class FullInfoWithoutCredentialsDto
{
    public string Username { get; init; }
    
    public UserInfoDto UserInfo { get; init; }
    
    public StudentInfoDto? StudentInfo { get; init; }
    
    public TrainerInfoDto? TrainerInfo { get; init; }
}