using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Student : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public Guid GroupId { get; private set; }
    public virtual Group Group { get; private set; } = null!;

    public float Balance { get; private set; }
    
    public DateTime BirthDate { get; private set; }
    
    public int SchoolGrade { get; private set; }
    
    public int Kyu { get; private set; }
    
    public DateTime KyuUpdatedAt { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; }
    
    public string Address { get; private set; }
    
    public string? firstParentName { get; private set; }
    public string? firstParentContact { get; private set; }
    
    public string? secondParentName { get; private set; }
    public string? secondParentContact { get; private set; }
    
    public Student(
        Guid userId,
        DateTime birthDate, 
        int schoolGrade, 
        int kyu,
        string address, 
        string? firstParentName = null, 
        string? firstParentContact = null, 
        string? secondParentName = null, 
        string? secondParentContact = null) : base(userId)
    {
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        TrainingStartDate = DateTime.UtcNow;
        UpdateKyu(kyu);
        Address = address;
        UserId = userId;
        this.firstParentName = firstParentName;
        this.firstParentContact = firstParentContact;
        this.secondParentName = secondParentName;
        this.secondParentContact = secondParentContact;
    }

    public void UpdateKyu(int kyu)
    {
        Kyu = kyu;
        KyuUpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(float balanceDiff)
    {
        Balance += balanceDiff;
    }

    public void ChangeGroup(Guid groupId)
    {
        GroupId = groupId;
    }
}