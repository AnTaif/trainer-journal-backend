using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums;

namespace TrainerJournal.Domain.Entities;

public class Session : Entity<Guid>
{
    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = null!;
    
    public float Price { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public bool IsCanceled { get; private set; }
    
    public string CancelComment { get; private set; }
    
    public SessionType SessionType { get; private set; }
    
    public Guid TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;
    
    public Guid HallId { get; private set; }
    public Hall Hall { get; private set; } = null!;
    
    public Session(
        Guid groupId, 
        float price, 
        DateTime startDate, 
        DateTime endDate, 
        bool isCanceled, 
        string cancelComment, 
        SessionType sessionType, 
        Guid trainerId, 
        Guid hallId) : base(Guid.NewGuid())
    {
        GroupId = groupId;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        IsCanceled = isCanceled;
        CancelComment = cancelComment;
        SessionType = sessionType;
        TrainerId = trainerId;
        HallId = hallId;
    }
}