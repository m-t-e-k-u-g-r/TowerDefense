namespace Game.Core.Entities.Tower;

using Domain.Field;

public abstract class PlacedTower(TilePosition position)
{
    private float _timeElapsed;
    protected abstract float Damage { get; }
    protected abstract float Range { get; }
    protected abstract float FireRate { get; }

    public void Update(float deltaTime, List<Enemy.Enemy> enemies)
    {
        _timeElapsed += deltaTime;
        var shots = (int)Math.Floor(_timeElapsed / FireRate);
        FindTarget(enemies, shots);
    }
    
    private void FindTarget(List<Enemy.Enemy> enemies, int shots)
    {
        if (enemies.Count == 0 || shots < 1) return;
        for (var i = 0; i < shots; i++)
        {
            var enemiesInRange = enemies.Select(e =>
                {
                    var dx = e.Position.XPos - position.XPos;
                    var dy = e.Position.YPos - position.YPos;
                    return Range > dx * dx + dy * dy ? e : null;
                })
                .OfType<Enemy.Enemy>()
                .ToList();
            if (enemiesInRange.Count <= 0) return;

            var target = enemiesInRange
                .OrderByDescending(e => e.PathProgress)
                .ThenBy(e => e.Health)
                .First();
            target.ReceiveDamage(Damage);
            _timeElapsed -= FireRate;
        }
    }
}