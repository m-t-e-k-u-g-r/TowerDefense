namespace Game.Entities.Enemy;

using Field;

public class Enemy(EnemyType type, Path path)
{
    public float Health { get; private set; } = type.MaxHealth;
    private int _pathIndex;
    private float _progress;
    public float PathProgress => path.GetPathProgress(_pathIndex);
    public Position Position => path.GetPosition(_pathIndex, _progress);

    public event Action<Enemy> OnDefeat;
    public event Action<Enemy> OnReach;

    public void Update(float deltaTime)
    {
        Move(deltaTime);
    }

    private void Move(float deltaTime)
    {
        var newProgress = _progress + type.MoveSpeed * deltaTime;
        var movedForward = (int)Math.Floor(newProgress);
        _progress = newProgress - movedForward;
        _pathIndex += movedForward;
        if (_pathIndex >= path.Tiles.Length)
        {
            OnReach.Invoke(this);
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (Random.Shared.NextDouble() < type.Evasion) return;
        Health -= damage;
        if (Health <= 0)
        {
            OnDefeat.Invoke(this);
        }
    }
}