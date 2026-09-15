namespace Game.Core.Entities.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigSpawnGroup
{
    public required Guid EnemyId { get; init; }
    [Range(1, int.MaxValue)]
    public required int EnemyCount { get; init; }
}