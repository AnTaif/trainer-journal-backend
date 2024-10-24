using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Trainers.Dtos;

namespace TrainerJournal.Application.Services.Users.Dtos.Responses;

public record GetUserInfoResponse(Guid Id, UserInfoDto UserInfo, StudentInfoDto? StudentInfo, TrainerInfoDto? TrainerInfo);