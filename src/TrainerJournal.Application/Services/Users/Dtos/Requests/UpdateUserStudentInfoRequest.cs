using TrainerJournal.Application.Services.Students.Dtos.Requests;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateUserStudentInfoRequest
{
    public UpdateUserInfoRequest? UserInfo { get; init; }
    
    public UpdateStudentInfoRequest? StudentInfo { get; init; }
}