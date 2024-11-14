namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateStudentRequest
{
    public UpdateUserInfoRequest? UserInfo { get; set; }
    
    public UpdateStudentInfoRequest? StudentInfo { get; set; }
}