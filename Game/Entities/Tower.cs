namespace Game.Entities;

using Field;

public class Tower(float damage, float range, float fireRate, Position position)
{
    private float _timeElapsed;

    public void Update(float deltaTime, List<Enemy.Enemy> enemies)
    {
        _timeElapsed += deltaTime;
        var shots = (int)Math.Floor(_timeElapsed / fireRate);
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
                    return range > dx * dx + dy * dy ? e : null;
                })
                .OfType<Enemy.Enemy>()
                .ToList();
            if (enemiesInRange.Count <= 0) return;

            var target = enemiesInRange
                .OrderByDescending(e => e.PathProgress)
                .ThenBy(e => e.Health)
                .First();
            target.ReceiveDamage(damage);
            _timeElapsed -= fireRate;
        }
    }
}