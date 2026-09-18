namespace Game.Domain.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigWave
{
    public required string Name { get; init; }
    [Range(1, int.MaxValue)]
    public required int Duration { get; init; }
    public required ConfigSpawnGroup[] Groups { get; init; }
}