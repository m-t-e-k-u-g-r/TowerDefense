namespace Game.Core.Entities.Tower;

using Config;
using Field;

public class FinalTower(TilePosition position, ConfigTower tower) : PlacedTower(position)
{
    protected override float Damage { get; } = tower.Damage;
    protected override float Range { get; } = tower.Range;
    protected override float FireRate { get; } = tower.FireRate;
}