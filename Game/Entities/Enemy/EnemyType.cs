namespace Game.Entities.Enemy;

public class EnemyType
{
    public int maxHealth { get; }
    public float moveSpeed { get; }
    public float evasion { get; }

    public EnemyType(int maxHealth, float moveSpeed, float evasion)
    {
        this.maxHealth = maxHealth;
        this.moveSpeed = moveSpeed;
        this.evasion = evasion;
    }
}