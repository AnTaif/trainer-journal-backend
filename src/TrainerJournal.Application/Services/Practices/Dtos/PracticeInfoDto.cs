namespace TrainerJournal.Application.Services.Practices.Dtos;

public record PracticeInfoDto(
    Guid Id, 
    DateTime StartDate, 
    DateTime EndDate, 
    bool IsCanceled, 
    string CancelComment, 
    string PracticeType,
    float Price, 
    PracticeTrainerInfoDto TrainerInfo,
    PracticeGroupInfoDto GroupInfo,
    PracticeHallInfoDto HallInfo);

public record PracticeTrainerInfoDto(Guid Id, string FullName);

public record PracticeGroupInfoDto(Guid Id, string Name); 

public record PracticeHallInfoDto(Guid Id, string Address);