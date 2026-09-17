namespace Game.Persistence.Entities;

public class TowerLevel
{
    public int Level { get; init; }

    public Guid TowerId { get; init; }

    public int Cost { get; init; }

    public decimal Damage { get; init; }

    public decimal Range { get; init; }

    public decimal FireRate { get; init; }
}
