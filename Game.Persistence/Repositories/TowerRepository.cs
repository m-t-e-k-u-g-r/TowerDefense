namespace Game.Persistence.Repositories;

using Context;
using Entities;

public class TowerRepository(TowerDefenseContext dbContext)
{
    private TowerDefenseContext DbContext => dbContext;

    public List<ComputedTower> GetTowerTypes() => [..DbContext.ComputedTowers];

    public FinalTower? GetFinalTower(int pathId) => DbContext.FinalTowers.FirstOrDefault(x => x.PathId == pathId);

    public ComputedTower? GetTowerType(Guid towerId) =>
        DbContext.ComputedTowers.FirstOrDefault(t => t.Id == towerId);
}