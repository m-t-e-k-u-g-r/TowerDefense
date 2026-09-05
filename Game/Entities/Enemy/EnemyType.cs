namespace Game.Entities.Enemy;

public class EnemyType(int maxHealth, float moveSpeed, float evasion)
{
    public int MaxHealth { get; } = maxHealth;
    public float MoveSpeed { get; } = moveSpeed;
    public float Evasion { get; } = evasion;
}