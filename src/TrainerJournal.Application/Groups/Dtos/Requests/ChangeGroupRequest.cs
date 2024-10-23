namespace TrainerJournal.Application.Groups.Dtos.Requests;

public record ChangeGroupRequest(string? Name, Guid? TrainerId, Guid? HallId);