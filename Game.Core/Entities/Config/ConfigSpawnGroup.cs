namespace Game.Core.Entities.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigSpawnGroup
{
    public required string Type { get; init; }
    [Range(1, int.MaxValue)]
    public required int EnemyCount { get; init; }
}