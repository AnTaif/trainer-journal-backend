using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Auth.Token;

public interface ITokenGenerator
{
    public string GenerateTokenAsync(User user, IEnumerable<string> roles);
}