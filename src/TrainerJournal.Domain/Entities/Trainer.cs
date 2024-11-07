using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Entities;

/// <remarks>
/// Use the UserId as the Primary Key
/// </remarks>
public class Trainer(Guid userId)
{
    /// <summary>
    /// PrimaryKey for Trainer and ForeignKey for the User table
    /// </summary>
    public Guid UserId { get; init; } = userId;
    [ForeignKey("UserId")]
    public User User { get; private set; } = null!;
}