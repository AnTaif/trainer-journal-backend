namespace TrainerJournal.Application.Groups.Dtos;

public record GroupDto(Guid Id, string Name, Guid TrainerId, Guid HallId);