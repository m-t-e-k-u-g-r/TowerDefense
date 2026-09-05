namespace Game.Entities;

using Field;

public class Tower
{
    private float damage;
    private float range;
    private float fireRate;
    private float timeElapsed;
    private Position position;

    public Tower(float damage, float range, float fireRate, Position position)
    {
        this.damage = damage;
        this.range = range;
        this.fireRate = fireRate;
        this.position = position;
    }

    public void Update(float deltaTime, List<Enemy.Enemy> enemies)
    {
        timeElapsed += deltaTime;
        int shots = (int)Math.Floor(timeElapsed / fireRate);
        FindTarget(enemies, shots);
    }

    void FindTarget(List<Enemy.Enemy> enemies, int shots)
    {
        if (enemies.Count == 0 || shots < 1) return;
        for (int i = 0; i < shots; i++)
        {
            List<Enemy.Enemy> enemiesInRange = enemies.Select(e =>
                {
                    var dx = e.Position.xPos - position.xPos;
                    var dy = e.Position.yPos - position.yPos;
                    if (range > dx * dx + dy * dy)
                    {
                        return e;
                    }
                    return null;
                })
                .OfType<Enemy.Enemy>()
                .ToList();
            if (enemiesInRange.Count <= 0)
            {
                return;
            }

            Enemy.Enemy target = enemiesInRange
                .OrderByDescending(e => e.PathProgress)
                .ThenBy(e => e.health)
                .First();
            target.ReceiveDamage(damage);
            timeElapsed -= fireRate;
        }
    }
}