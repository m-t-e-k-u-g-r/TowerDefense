namespace Game.Domain.Config;

using Field;

public record ConfigTower
{
    public required float Damage { get; init; }
    public required float Range { get; init; }
    public required float FireRate { get; init; }
    public required TilePosition Position { get; init; }
}