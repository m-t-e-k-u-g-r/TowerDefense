namespace Game.Domain.Config;

using Field;
using System.ComponentModel.DataAnnotations;

public record ConfigPath
{
    [Required]
    public required ConsoleColor Color { get; init; }
    [Required]
    public required TilePosition[] Tiles { get; init; }
}