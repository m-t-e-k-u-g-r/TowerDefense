using Game.Entities.Enemy;

namespace Game.Waves;

public class SpawnRequest
{
    public EnemyType type { get; }
    public int count { get; }

    public SpawnRequest(EnemyType type, int count)
    {
        this.type = type;
        this.count = count;
    }
}