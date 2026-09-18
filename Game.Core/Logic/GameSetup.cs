namespace Game.Core.Logic;

using Domain.Config;
using Domain.Field;
using Domain.Field.Tiles;
using Entities.Tower;
using Field.Tiles;
using Waves;

public class GameSetup
{
    public Game CreateGameFromConfig(GameConfig config)
    {
        var tiles = new Tile[config.Field.Width, config.Field.Height];
        for (var x = 0; x < config.Field.Width; x++)
        {
            for (var y = 0; y < config.Field.Height; y++) tiles[x, y] = new TowerTile(x, y, null);
        }

        Path[] paths = [..config.Paths.Select(p => new Path(
            [..p.Tiles.Select(pos =>
            {
                var pathTile = new PathTile(pos.XPos, pos.YPos);
                tiles[pos.XPos, pos.YPos] = pathTile;
                return pathTile;
            })],
            p.Color
        ))];

        var field = new Core.Field.Field(tiles, [..paths]);

        var enemyTypes = config.EnemyTypes.ToDictionary(t => t.Id);

        Wave[] waves = [..config.Waves.Select(w => new Wave(
            w.Name,
            w.Duration,
            [.. w.Groups.Select(s => new SpawnGroup(enemyTypes[s.EnemyId], s.Count))]
        ))];

        foreach (var tower in config.Towers) PlaceTower(field, tower);
        return new Game(field, config.TowerTypes, waves);
    }

    private static void PlaceTower(Core.Field.Field field, ConfigTower tower)
    {
        var pos = tower.Position;
        if (field.Tiles[pos.XPos, pos.YPos] is not PathTile)
        {
            field.Tiles[pos.XPos, pos.YPos] = new TowerTile(
                pos.XPos, pos.YPos,
                new FinalTower(pos, tower)
            );
        }
    }
}