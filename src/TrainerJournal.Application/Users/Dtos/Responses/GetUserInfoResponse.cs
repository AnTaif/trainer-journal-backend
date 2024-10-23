using TrainerJournal.Application.Students.Dtos;
using TrainerJournal.Application.Trainers.Dtos;

namespace TrainerJournal.Application.Users.Dtos.Responses;

public record GetUserInfoResponse(Guid Id, UserInfoDto UserInfo, StudentInfoDto? StudentInfo, TrainerInfoDto? TrainerInfo);