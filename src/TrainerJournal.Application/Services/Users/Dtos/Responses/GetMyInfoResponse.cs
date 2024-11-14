using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Trainers.Dtos;

namespace TrainerJournal.Application.Services.Users.Dtos.Responses;

public class GetMyInfoResponse
{
    public Guid Id { get; set; }
    public UserInfoDto UserInfo { get; set; }
    public StudentInfoDto? StudentInfo { get; set; }
    public TrainerInfoDto? TrainerInfo { get; set; }
}