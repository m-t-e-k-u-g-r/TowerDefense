namespace Game.Persistence.Entities;

public class Enemy
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public int MaxHealth { get; init; }

    public decimal MoveSpeed { get; init; }

    public decimal Evasion { get; init; }

    public int Reward { get; init; }
}
