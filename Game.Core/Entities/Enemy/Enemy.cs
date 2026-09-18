namespace Game.Core.Entities.Enemy;

using Domain.Entities.Enemy;
using Domain.Field;

public class Enemy(EnemyType type, Path path)
{
    public readonly EnemyType Type = type;
    public float Health { get; private set; } = type.MaxHealth;
    private int _pathIndex;
    private float _progress;
    public float PathProgress => path.GetPathProgress(_pathIndex);
    public Position Position => path.GetPosition(_pathIndex, _progress);

    public event Action<Enemy>? OnDefeat;
    public event Action<float>? OnHit;
    public event Action<Enemy>? OnReach;

    public void Update(float deltaTime) => Move(deltaTime);

    private void Move(float deltaTime)
    {
        var newProgress = _progress + Type.MoveSpeed * deltaTime;
        var movedForward = (int)Math.Floor(newProgress);
        _progress = newProgress - movedForward;
        _pathIndex += movedForward;
        if (_pathIndex >= path.Tiles.Length)
        {
            OnReach?.Invoke(this);
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (Random.Shared.NextDouble() < Type.Evasion) return;
        Health -= damage;
        OnHit?.Invoke(damage);
        if (Health <= 0)
        {
            OnDefeat?.Invoke(this);
        }
    }
}