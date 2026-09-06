namespace Game.Entities.Enemy;

public class EnemyType(int maxHealth, float moveSpeed, float evasion, int reward)
{
    public int MaxHealth { get; } = maxHealth;
    public float MoveSpeed { get; } = moveSpeed;
    public float Evasion { get; } = evasion;
    public int Reward { get; } = reward;
}