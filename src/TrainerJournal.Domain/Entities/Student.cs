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
    public int SchoolGrade { get; private set; }
    
    public int? Kyu { get; private set; }
    public DateTime? KyuUpdatedAt { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; } = DateTime.UtcNow;
    public string Address { get; private set; }

    public List<ExtraContact> ExtraContacts { get; set; } = null!;

    public Student() : base(Guid.NewGuid()) { }
    
    public Student(
        Guid userId,
        DateTime birthDate, 
        int schoolGrade, 
        int? kyu,
        string? address, 
        List<ExtraContact> extraContacts) : base(userId)
    {
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        UpdateKyu(kyu);
        Address = address ?? "";
        UserId = userId;
        ExtraContacts = extraContacts;
    }

    public void Update(
        DateTime? birthDate = null, 
        int? schoolGrade = null,
        string? address = null,
        int? kyu = null,
        List<(string? Name, string? Contact)>? extraContacts = null)
    {
        BirthDate = birthDate ?? BirthDate;
        SchoolGrade = schoolGrade ?? SchoolGrade;
        Address = address ?? Address;

         UpdateKyu(kyu);
        
        if (extraContacts != null)
        {
            ExtraContacts.Clear();
            foreach (var ex in extraContacts)
            {
                ExtraContacts.Add(new ExtraContact(ex.Name, ex.Contact));
            }
        }
    }

    public void UpdateKyu(int? kyu)
    {
        if (kyu == null || Kyu == kyu) return;
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