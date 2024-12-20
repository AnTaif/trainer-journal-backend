using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Events;
using TrainerJournal.Domain.ValueObjects;

namespace TrainerJournal.Domain.Entities;

public class Group : Entity<Guid>
{
    public string Name { get; private set; } = null!;
    public HexColor HexColor { get; private set; } = null!;

    public string HallAddress { get; private set; } = "";
    
    public float Price { get; private set; }
    
    public bool IsDeleted { get; private set; }
    
    public Guid TrainerId { get; private set; }
    public Trainer Trainer { get; private set; } = null!;

    public List<Student> Students { get; private set; } = [];
    
    public Group() : base(Guid.NewGuid()) { }
    
    public Group(string name, float price, string hallAddress, HexColor hexColor, Guid trainerId) : base(Guid.NewGuid())
    {
        Name = name;
        Price = price;
        HallAddress = hallAddress;
        HexColor = hexColor;
        TrainerId = trainerId;
    }

    public void UpdateInfo(string? name, float? price, string? hallAddress, string? hexCode)
    {
        if (name != null) Name = name;
        if (hallAddress != null && HallAddress != hallAddress) ChangeAddress(hallAddress);
        if (price != null && Math.Abs(price.Value - Price) > 0.0001) SetPrice(price.Value);
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

    public void ChangeAddress(string hallAddress)
    {
        AddDomainEvent(new GroupChangedEvent(this, null, hallAddress));
        HallAddress = hallAddress;
    }

    public void SetPrice(float price)
    {
        if (Math.Abs(price - Price) < 0.0001) return;
        
        AddDomainEvent(new GroupChangedEvent(this, price, null));
        Price = price;
    }
}