using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

public class Practice : Entity<Guid>
{
    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = null!;
    
    public float Price { get; private set; }
    
    public DateTime StartDate { get; private set; }
    
    public DateTime EndDate { get; private set; }
    
    public bool IsCanceled { get; private set; }

    public string CancelComment { get; private set; } = "";
    
    public PracticeType PracticeType { get; private set; }
    
    public Guid TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;
    
    public Guid HallId { get; private set; }
    public Hall Hall { get; private set; } = null!;
    
    public Practice(
        Guid groupId, 
        float price, 
        DateTime startDate, 
        DateTime endDate,
        PracticeType practiceType, 
        Guid trainerId, 
        Guid hallId) : base(Guid.NewGuid())
    {
        GroupId = groupId;
        Price = price;
        StartDate = startDate;
        EndDate = endDate;
        PracticeType = practiceType;
        TrainerId = trainerId;
        HallId = hallId;
    }

    public void Cancel(string comment = "")
    {
        IsCanceled = true;
        CancelComment = comment;
    }
}