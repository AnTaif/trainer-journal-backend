using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public record CreateScheduleRequest(
    [Range(0, float.MaxValue)]
    float Price,
    [Range(0, int.MaxValue)]
    int RepeatWeeks,
    List<CreatePracticeItemRequest> Practices);