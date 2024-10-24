using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Auth.Tokens;

public interface ITokenGenerator
{
    public string GenerateToken(User user, IEnumerable<string> roles);
}