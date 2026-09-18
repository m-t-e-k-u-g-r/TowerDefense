namespace Game.Core.Entities.Tower;

using Domain.Entities.Tower;
using Domain.Field;

public class RegularTower(TowerType type, int level, TilePosition position) : PlacedTower(position)
{
    public int Level = level;
    public TowerType Type { get; } = type;
    private TowerLevel CurrentLevel =>
        Type.GetLevel(Level)
        ?? throw new InvalidOperationException("Invalid tower level");
    public TowerLevel? NextLevel => Type.GetLevel(Level + 1);
    protected override float Damage => CurrentLevel.Damage;
    protected override float Range => CurrentLevel.Range;
    protected override float FireRate => CurrentLevel.FireRate;

    public void Upgrade() { Level++; }
}