using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Group : Entity<Guid>
{
    public string Name { get; private set; }
    public HexColor HexColor { get; private set; }
    
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
            student.ChangeGroup(null);
        }
    }

    public void SetPrice(float price)
    {
        Price = price;
    }
}