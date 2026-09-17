namespace Game.Persistence.Entities;

public class FinalTower
{
    public int Id { get; init; }

    public int PathId { get; init; }

    public decimal Damage { get; init; }

    public decimal Range { get; init; }

    public decimal FireRate { get; init; }

    public int XPos { get; init; }

    public int YPos { get; init; }
}
