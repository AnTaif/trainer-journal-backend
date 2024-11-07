using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Entities;

/// <remarks>
/// Use the UserId as the Primary Key
/// </remarks>
public class Student
{
    /// <summary>
    /// PrimaryKey for Student and ForeignKey for the User table
    /// </summary>
    public Guid UserId { get; private set; }
    [ForeignKey("UserId")]
    public User User { get; private set; } = null!;
    
    public Guid? GroupId { get; private set; }
    public virtual Group? Group { get; private set; }
    
    public float Balance { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int SchoolGrade { get; private set; }
    
    public int? Kyu { get; private set; }
    public DateTime? KyuUpdatedAt { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; } = DateTime.UtcNow;
    public string Address { get; private set; } = null!;

    public List<ExtraContact> ExtraContacts { get; set; } = null!;

    public Student() { }
    
    public Student(
        Guid userUserId,
        DateTime birthDate, 
        int schoolGrade, 
        int? kyu,
        string? address, 
        List<ExtraContact> extraContacts)
    {
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        UpdateKyu(kyu);
        Address = address ?? "";
        UserId = userUserId;
        ExtraContacts = extraContacts;
    }

    public void Update(
        DateTime? birthDate = null, 
        int? schoolGrade = null,
        string? address = null,
        int? kyu = null,
        List<(string Name, string Contact)>? extraContacts = null)
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