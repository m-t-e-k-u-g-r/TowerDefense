namespace Game.Core.Entities.Config;

using Enemy;
using Tower;

public record GameConfig
{
    public required ConfigField Field { get; init; }
    public required ConfigPath[] Paths { get; init; }
    public required EnemyType[] EnemyTypes { get; init; }
    public required ConfigWave[] Waves { get; init; }
    public required TowerType[] TowerTypes { get; init; }
    public required ConfigTower[] Towers { get; init; }
}