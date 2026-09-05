namespace Game.Entities.Enemy;

using Field;

public class Enemy
{
    private EnemyType type;
    public float health { get; private set; }
    private Path path;
    private int pathIndex;
    private float progress;
    public float PathProgress => path.GetPathProgress(pathIndex);
    public Position Position => path.GetPosition(pathIndex, progress);

    public event Action<Enemy> OnDefeat;
    public event Action<Enemy> OnReach;

    public Enemy(EnemyType type, Path path)
    {
        this.type = type;
        health = type.maxHealth;
        this.path = path;
        pathIndex = 0;
        progress = 0;
    }

    public void Update(float deltaTime)
    {
        Move(deltaTime);
    }

    void Move(float deltaTime)
    {
        float newProgress = progress + type.moveSpeed * deltaTime;
        int movedForward = (int)Math.Floor(newProgress);
        progress = newProgress - movedForward;
        pathIndex += movedForward;
        if (pathIndex >= path.tiles.Length)
        {
            OnReach?.Invoke(this);
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (Random.Shared.NextDouble() > type.evasion)
        {
            health -= damage;
            if (health <= 0)
            {
                OnDefeat?.Invoke(this);
            }
        }
    }
}