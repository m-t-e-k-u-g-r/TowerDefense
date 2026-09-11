namespace Game.Core.Entities.Tower;

using Field;

public class Tower(TowerType type, int level, TilePosition position)
{
    private float _timeElapsed;
    public int Level = level;
    public TowerType Type { get; } = type;
    private TowerLevel CurrentLevel =>
        Type.GetLevel(Level)
        ?? throw new InvalidOperationException("Invalid tower level");
    public bool CanUpgrade => Level < Type.Levels.Length;

    public void Update(float deltaTime, List<Enemy.Enemy> enemies)
    {
        _timeElapsed += deltaTime;
        var shots = (int)Math.Floor(_timeElapsed / CurrentLevel.FireRate);
        FindTarget(enemies, shots);
    }

    public void Upgrade() { Level++; }

    private void FindTarget(List<Enemy.Enemy> enemies, int shots)
    {
        if (enemies.Count == 0 || shots < 1) return;
        for (var i = 0; i < shots; i++)
        {
            var enemiesInRange = enemies.Select(e =>
                {
                    var dx = e.Position.XPos - position.XPos;
                    var dy = e.Position.YPos - position.YPos;
                    return CurrentLevel.Range > dx * dx + dy * dy ? e : null;
                })
                .OfType<Enemy.Enemy>()
                .ToList();
            if (enemiesInRange.Count <= 0) return;

            var target = enemiesInRange
                .OrderByDescending(e => e.PathProgress)
                .ThenBy(e => e.Health)
                .First();
            target.ReceiveDamage(CurrentLevel.Damage);
            _timeElapsed -= CurrentLevel.FireRate;
        }
    }
}