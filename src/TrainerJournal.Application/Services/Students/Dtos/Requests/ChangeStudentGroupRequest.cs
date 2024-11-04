namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class ChangeStudentGroupRequest(Guid? groupId)
{
    public Guid? GroupId { get; init; } = groupId;
}