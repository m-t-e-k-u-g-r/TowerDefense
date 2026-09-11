using System.ComponentModel.DataAnnotations;

namespace Game.Console.Models.Config;

public record ConfigWave
{
    public required string Name { get; init; }
    [Range(1, int.MaxValue)]
    public required int Duration { get; init; }
    public required ConfigSpawnGroup[] Groups { get; init; }
}