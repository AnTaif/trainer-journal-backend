using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record CreateUserRequest(
    string FullName,
    [EmailAddress]
    string? Email,
    [Phone]
    string? Phone, 
    [GenderEnum]
    string Gender);