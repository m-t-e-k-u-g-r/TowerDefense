namespace Game.Persistence.Entities;

public class SpawnGroup
{
    public int Id { get; init; }

    public Guid EnemyId { get; init; }

    public int Count { get; init; }
}
