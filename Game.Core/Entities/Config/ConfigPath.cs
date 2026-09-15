namespace Game.Core.Entities.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigPath
{
    [Required]
    public required ConsoleColor Color { get; init; }
    [Required]
    public required (int, int)[] Tiles { get; init; }
}