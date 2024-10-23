using TrainerJournal.Application.Students.Dtos;
using TrainerJournal.Application.Trainers.Dtos;

namespace TrainerJournal.Application.Users.Dtos.Responses;

public record GetUserInfoResponse(UserInfoDto UserInfo, StudentInfoDto? StudentInfo, TrainerInfoDto? TrainerInfo);