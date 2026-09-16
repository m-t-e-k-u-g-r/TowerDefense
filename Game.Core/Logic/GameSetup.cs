namespace Game.Core.Logic;

using Entities.Config;
using Entities.Enemy;
using Entities.Tower;
using Field;
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
                var pathTile = new PathTile(pos.Item1, pos.Item2);
                tiles[pos.Item1, pos.Item2] = pathTile;
                return pathTile;
            })],
            p.Color
        ))];
        
        var field = new Field(tiles, [..paths]);

        var enemyTypes = config.EnemyTypes.ToDictionary(t => t.Id);

        Wave[] waves = [..config.Waves.Select(w => new Wave(
            w.Name,
            w.Duration,
            [.. w.Groups.Select(s => new SpawnGroup(enemyTypes[s.EnemyId], s.EnemyCount))]
        ))];

        foreach (var tower in config.Towers) PlaceTower(field, tower);
        return new Game(field, config.TowerTypes, waves);
    }

    private static void PlaceTower(Field field, ConfigTower tower)
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