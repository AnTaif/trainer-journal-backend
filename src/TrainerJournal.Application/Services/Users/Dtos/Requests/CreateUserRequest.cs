using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record CreateUserRequest(
    string FullName, 
    [GenderEnum]
    string Gender);