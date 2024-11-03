using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public record CreateExtraContactRequest(
    string? Name, 
    [Phone]
    string? Contact);