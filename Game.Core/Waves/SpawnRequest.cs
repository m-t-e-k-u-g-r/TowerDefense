namespace Game.Core.Waves;

using Entities.Enemy;

public class SpawnRequest(EnemyType type, int count)
{
    public EnemyType Type { get; } = type;
    public int Count { get; } = count;
}