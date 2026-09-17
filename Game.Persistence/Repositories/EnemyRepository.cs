namespace Game.Persistence.Repositories;

using Context;
using Entities;

public class EnemyRepository(TowerDefenseContext dbContext)
{
    private TowerDefenseContext DbContext => dbContext;

    public List<Enemy> GetEnemyTypes() => [..DbContext.Enemies];

    public async Task<Enemy?> GetEnemyType(Guid enemyId) => await DbContext.Enemies.FindAsync(enemyId);
}