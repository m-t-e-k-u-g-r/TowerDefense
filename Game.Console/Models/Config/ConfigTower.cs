using System.ComponentModel.DataAnnotations;

namespace Game.Console.Models.Config;

using Field;

public record ConfigTower
{
    public required int Type { get; init; }
    [Range(1, int.MaxValue)]
    public required int Level { get; init; }
    public required TilePosition Position { get; init; }
}