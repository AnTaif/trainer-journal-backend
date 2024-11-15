using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Entities;

/// <remarks>
/// Use the UserId as the Primary Key
/// </remarks>
public class Trainer(Guid userId, string? phone = null, string? email = null)
{
    /// <summary>
    /// PrimaryKey for Trainer and ForeignKey for the User table
    /// </summary>
    public Guid UserId { get; init; } = userId;
    [ForeignKey("UserId")]
    public User User { get; private set; } = null!;

    public string? Phone { get; private set; } = phone;

    public string? Email { get; private set; } = email;

    public void Update(string? phone, string? email)
    {
        Phone = phone ?? Phone;
        Email = email ?? Email;
    }
}