using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public record ParentInfoDto(
    string? Name, 
    [Phone]
    string? Contact);