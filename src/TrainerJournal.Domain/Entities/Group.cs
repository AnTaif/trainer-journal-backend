using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Domain.Entities;

public class Group : Entity<Guid>
{
    public string Name { get; private set; } = null!;
    public HexColor HexColor { get; private set; } = null!;
    
    public float Price { get; private set; }
    
    public bool IsDeleted { get; private set; }
    
    public Guid TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;

    public List<Student> Students { get; private set; } = [];
    
    public Group() : base(Guid.NewGuid()) { }
    
    public Group(string name, HexColor hexColor, Guid trainerId) : base(Guid.NewGuid())
    {
        Name = name;
        HexColor = hexColor;
        TrainerId = trainerId;
    }

    public void UpdateInfo(string? name, string? hexCode)
    {
        if (name != null) Name = name;
        if (hexCode != null) HexColor = new HexColor(hexCode);
    }

    public void Delete()
    {
        IsDeleted = true;
        foreach (var student in Students)
        {
            student.ExcludeFromGroup(this);
        }
    }

    public void SetPrice(float price)
    {
        if (Math.Abs(price - Price) < 0.0001) return;
        
        AddDomainEvent(new GroupPriceChangedEvent(this, Price, price));
        Price = price;
    }
}