namespace Game.Waves;

using Entities.Enemy;

public class SpawnGroup(EnemyType type, int enemyCount)
{
    public EnemyType Type { get; } = type;
    public int EnemyCount { get; } = enemyCount;
    public int SpawnCount { get; set; }
}