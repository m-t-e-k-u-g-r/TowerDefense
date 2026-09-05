namespace Game.Waves;

using Entities.Enemy;

public class SpawnGroup
{
    public EnemyType type { get; }
    public int enemyCount { get; }
    public int spawnCount { get; set; }

    public SpawnGroup(EnemyType type, int enemyCount)
    {
        this.type = type;
        this.enemyCount = enemyCount;
        spawnCount = 0;
    }
}