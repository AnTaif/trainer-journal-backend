using System.ComponentModel.DataAnnotations.Schema;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

/// <remarks>
/// Use the UserId as the Primary Key
/// </remarks>
public class Trainer : Entity<Guid>
{
    [ForeignKey("Id")]
    public User User { get; private set; } = null!;

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public Trainer() : base(Guid.NewGuid()) { }
    
    /// <remarks>
    /// Use the UserId as the Primary Key
    /// </remarks>
    public Trainer(Guid userId, string? phone = null, string? email = null) : base(userId)
    {
        Phone = phone;
        Email = email;
    }

    public void Update(string? phone, string? email)
    {
        Phone = phone ?? Phone;
        Email = email ?? Email;
    }
}