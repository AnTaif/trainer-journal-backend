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
    
    public ParentInfo? FirstParent { get; set; }
    public ParentInfo? SecondParent { get; set; }
    
    public Student() : base(Guid.NewGuid()) { }
    
    public Student(
        Guid userId,
        DateTime birthDate, 
        int schoolGrade, 
        int kyu,
        string address, 
        ParentInfo? firstParent = null,
        ParentInfo? secondParent = null) : base(userId)
    {
        var curr = DateTime.UtcNow;
        
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        TrainingStartDate = curr;
        Kyu = kyu;
        KyuUpdatedAt = curr;
        Address = address;
        UserId = userId;
        FirstParent = firstParent;
        SecondParent = secondParent;
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
        
        if (FirstParent == null)
            FirstParent = firstParent;
        else
            FirstParent.Update(firstParent?.Name, firstParent?.Contact);
        
        if (SecondParent == null)
            SecondParent = secondParent;
        else
            SecondParent.Update(secondParent?.Name, secondParent?.Contact);
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

    public void ChangeGroup(Guid groupId)
    {
        GroupId = groupId;
    }
}