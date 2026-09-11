namespace Game.Console.Models.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigPath
{
    [Required]
    public required ConsoleColor Color { get; init; }
    [Required]
    public required int[][] Tiles { get; init; }
}