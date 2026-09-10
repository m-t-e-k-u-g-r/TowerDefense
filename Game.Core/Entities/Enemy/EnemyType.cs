namespace Game.Core.Entities.Enemy;

public class EnemyType(string id, int maxHealth, float moveSpeed, float evasion, int reward)
{
    public string Id { get; } = id;
    public int MaxHealth { get; } = maxHealth;
    public float MoveSpeed { get; } = moveSpeed;
    public float Evasion { get; } = evasion;
    public int Reward { get; } = reward;
}