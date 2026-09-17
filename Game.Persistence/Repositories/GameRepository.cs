namespace Game.Persistence.Repositories;

using Context;
using Entities;

public class GameRepository(TowerDefenseContext dbContext)
{
    private TowerDefenseContext DbContext => dbContext;

    public GameConfig GetGameConfig(Guid gameConfigId) =>
        DbContext.GameConfigs.First(gc => gc.Id == gameConfigId)
        ?? throw new KeyNotFoundException("Game config not found");
}