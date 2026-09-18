namespace Game.Domain.Entities.Enemy;

public class EnemyType(Guid id, string name, int maxHealth, float moveSpeed, float evasion, int reward)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public int MaxHealth { get; } = maxHealth;
    public float MoveSpeed { get; } = moveSpeed;
    public float Evasion { get; } = evasion;
    public int Reward { get; } = reward;
}