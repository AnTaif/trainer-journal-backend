namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public record ChangeGroupRequest(string? Name, Guid? TrainerId, Guid? HallId);