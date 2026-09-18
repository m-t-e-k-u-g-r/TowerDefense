namespace Game.Domain.Config;

public record ConfigSpawnGroup
{
    public required Guid EnemyId { get; init; }
    public required int Count { get; init; }
}