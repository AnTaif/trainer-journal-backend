using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Trainers.Dtos.Requests;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record UpdateUserRequest(UpdateUserInfoRequest? UserInfo, UpdateStudentInfoRequest? StudentInfo, UpdateTrainerInfoRequest? TrainerInfo);