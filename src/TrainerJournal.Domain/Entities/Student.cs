using System.ComponentModel.DataAnnotations.Schema;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.BalanceChangeReason;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Domain.Entities;

/// <remarks>
/// Use the UserId as the Primary Key
/// </remarks>
public class Student : Entity<Guid>
{
    [ForeignKey("Id")]
    public User User { get; private set; } = null!;

    public List<Group> Groups { get; private set; } = new();
    
    public float Balance { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int SchoolGrade { get; private set; }
    
    public int? Kyu { get; private set; }
    public DateTime? KyuUpdatedAt { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; } = DateTime.UtcNow;
    public string Address { get; private set; } = null!;

    public List<Contact> Contacts { get; set; } = null!;

    public Student() : base(Guid.NewGuid()) { }
    
    public Student(
        Guid userId,
        DateTime birthDate, 
        int schoolGrade, 
        int? kyu,
        string? address, 
        List<Contact> contacts) : base(userId)
    {
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        UpdateKyu(kyu);
        Address = address ?? "";
        Id = userId;
        Contacts = contacts;
    }

    public void Update(
        DateTime? birthDate = null, 
        int? schoolGrade = null,
        string? address = null,
        int? kyu = null)
    {
        BirthDate = birthDate ?? BirthDate;
        SchoolGrade = schoolGrade ?? SchoolGrade;
        Address = address ?? Address;

         UpdateKyu(kyu);
    }

    public void UpdateContacts(List<Contact>? contacts)
    {
        if (contacts == null) return;
        
        Contacts.Clear();
        foreach (var contact in contacts)
        {
            Contacts.Add(new Contact(contact.Name, contact.Relation, contact.Phone));
        }
    }

    public void UpdateKyu(int? kyu)
    {
        if (kyu == null || Kyu == kyu) return;
        //TODO: add event
        Kyu = kyu;
        KyuUpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(float balanceDiff, BalanceChangeReason reason, DateTime eventDate)
    {
        AddDomainEvent(new BalanceChangedEvent(Id, balanceDiff, Balance, reason, eventDate));
        Balance += balanceDiff;
    }

    public void AddToGroup(Group group)
    {
        if (Groups.Contains(group)) return;
        Groups.Add(group);
    }

    public void ExcludeFromGroup(Group group)
    {
        if (!Groups.Contains(group)) return;
        Groups.Remove(group);
    }
}