namespace TrainerJournal.Application.Groups.Dtos;

public record GroupItemDto(Guid Id, string Name, Guid TrainerId, Guid HallId);