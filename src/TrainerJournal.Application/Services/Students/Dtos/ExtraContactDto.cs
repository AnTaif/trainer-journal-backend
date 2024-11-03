using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public record ExtraContactDto(
    string? Name, 
    [Phone]
    string? Contact);