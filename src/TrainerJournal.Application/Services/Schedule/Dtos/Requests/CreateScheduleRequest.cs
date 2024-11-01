namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public record CreateScheduleRequest(
    Guid GroupId,
    float Price,
    string PracticeType,
    Guid TrainerId,
    Guid HallId,
    DateTime StartMonday,
    DateTime? Until,
    List<CreateSchedulePracticeRequest> Practices);