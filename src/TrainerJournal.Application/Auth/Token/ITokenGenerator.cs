using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Auth.Token;

public interface ITokenGenerator
{
    public string GenerateToken(User user, IEnumerable<string> roles);
}