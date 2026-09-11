namespace Game.Console.Models.Config;

using System.ComponentModel.DataAnnotations;

public record ConfigField
{
    [Range(1, int.MaxValue)]
    public required int Width { get; init; }
    [Range(1, int.MaxValue)]
    public required int Height { get; init; }
}