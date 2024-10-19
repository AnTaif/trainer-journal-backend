using TrainerJournal.Domain.Entities.Abstract;

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
    
    public int AikidoGrade { get; private set; }
    
    public DateTime LastAikidoGradeDate { get; private set; }
    
    public DateTime TrainingStartDate { get; private set; }
    
    public string Address { get; private set; }
    
    public string? firstParentName { get; private set; }
    public string? firstParentContact { get; private set; }
    
    public string? secondParentName { get; private set; }
    public string? secondParentContact { get; private set; }
    
    public Student(
        Guid userId,
        Guid groupId,
        float balance, 
        DateTime birthDate, 
        int schoolGrade, 
        int aikidoGrade, 
        DateTime lastAikidoGradeDate, 
        DateTime trainingStartDate, 
        string address, string? firstParentName = null, 
        string? firstParentContact = null, 
        string? secondParentName = null, 
        string? secondParentContact = null) : base(Guid.NewGuid())
    {
        GroupId = groupId;
        Balance = balance;
        BirthDate = birthDate;
        SchoolGrade = schoolGrade;
        AikidoGrade = aikidoGrade;
        LastAikidoGradeDate = lastAikidoGradeDate;
        TrainingStartDate = trainingStartDate;
        Address = address;
        UserId = userId;
        this.firstParentName = firstParentName;
        this.firstParentContact = firstParentContact;
        this.secondParentName = secondParentName;
        this.secondParentContact = secondParentContact;
    }
}