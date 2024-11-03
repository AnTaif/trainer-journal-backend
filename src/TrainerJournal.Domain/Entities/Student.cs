using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Student : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public Guid? GroupId { get; private set; }
    public virtual Group Group { get; private set; } = null!;
    
    public float Balance { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int? SchoolGrade { get; private set; }
    
    public int? Kyu { get; private set; }
    public DateTime? KyuUpdatedAt { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; }
    public string? Address { get; private set; }
    
    public ParentInfo FirstParent { get; set; } = null!;
    public ParentInfo SecondParent { get; set; } = null!;

    public Student() : base(Guid.NewGuid()) { }
    
    public Student(
        Guid userId,
        DateTime birthDate, 
        int? schoolGrade, 
        int? kyu,
        string? address, 
        ParentInfo firstParent,
        ParentInfo? secondParent = null) : base(userId)
    {
        var curr = DateTime.UtcNow;
        
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        TrainingStartDate = curr;
        Kyu = kyu;
        KyuUpdatedAt = kyu == null ? null : curr;
        Address = address;
        UserId = userId;
        FirstParent = firstParent;
        SecondParent = secondParent ?? new ParentInfo("", "");
    }

    public void Update(
        DateTime? birthDate = null, 
        int? schoolGrade = null,
        string? address = null,
        ParentInfo? firstParent = null,
        ParentInfo? secondParent = null)
    {
        BirthDate = birthDate ?? BirthDate;
        SchoolGrade = schoolGrade ?? SchoolGrade;
        Address = address ?? Address;
        
        if (firstParent != null) FirstParent.Update(firstParent);
        if (secondParent != null) SecondParent.Update(secondParent);
    }

    public void UpdateKyu(int kyu)
    {
        //TODO: add event
        Kyu = kyu;
        KyuUpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(float balanceDiff)
    {
        Balance += balanceDiff;
    }

    public void ChangeGroup(Guid? groupId)
    {
        GroupId = groupId;
    }
}