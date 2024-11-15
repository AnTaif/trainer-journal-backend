using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Trainers.Dtos.Requests;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateFullInfoRequest
{
    public UpdateUserInfoRequest? UserInfo { get; set; }
    
    public UpdateStudentInfoRequest? StudentInfo { get; set; }
    
    public UpdateTrainerInfoRequest? TrainerInfo { get; set; }
}