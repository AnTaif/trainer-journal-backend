using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Trainers.Dtos.Requests;

public class UpdateTrainerInfoRequest
{
    [Phone]
    public string? Phone { get; init; }
    
    [EmailAddress]
    public string? Email { get; init; }
}