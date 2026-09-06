namespace Game.Entities.Tower;

public class TowerLevel(int cost, float damage, float range, float fireRate)
{
    public int Cost => cost;
    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;
}