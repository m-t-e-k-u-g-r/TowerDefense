namespace Game.Persistence.Repositories;

using Context;
using Entities;

public class WaveRepository(TowerDefenseContext dbContext)
{
    private TowerDefenseContext DbContext => dbContext;

    public List<ComputedWave> GetWaves(Guid gameId) =>
        [.. DbContext.ComputedWaves.Where(w => w.GameId == gameId)];
}