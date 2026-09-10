using Game.Entities.Enemy;

namespace Game.Waves;

public class SpawnRequest(EnemyType type, int count)
{
    public EnemyType Type { get; } = type;
    public int Count { get; } = count;
}