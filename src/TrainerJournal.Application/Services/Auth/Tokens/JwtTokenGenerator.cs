using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Options;

namespace TrainerJournal.Application.Services.Auth.Tokens;

public class JwtTokenGenerator(IOptions<JwtOptions> options) : ITokenGenerator
{
    public string GenerateToken(User user, IEnumerable<string> roles)
    {
        var claims = CreateClaims(user, roles);
        var signingCredentials = CreateSigningCredentials();
        var token = CreateJwtToken(claims, signingCredentials);

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        return jwtTokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(
        List<Claim> claims, 
        SigningCredentials signingCredentials)
    {
        return new JwtSecurityToken(
            options.Value.Issuer,
            options.Value.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpiryMinutes),
            signingCredentials: signingCredentials);
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.Secret)), 
            SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> CreateClaims(User user, IEnumerable<string> roles)
    {
        if (user.UserName == null)
            throw new ArgumentNullException(nameof(user), "Username cannot be null");
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.Sid, user.Id.ToString()), 
            new(JwtRegisteredClaimNames.Sub, user.UserName)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        return claims;
    }
}