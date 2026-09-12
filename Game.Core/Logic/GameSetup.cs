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
            for (var y = 0; y < config.Field.Height; y++)
            {
                tiles[x, y] = new TowerTile(x, y);
            }
        }

        List<PathTile> pathPoints = new();
        List<Path> paths = [];
        foreach (var path in config.Paths)
        {
            foreach (var tile in path.Tiles)
            {
                var x = tile[0];
                var y = tile[1];
                AddPathTile(x, y, tiles, pathPoints);
            }
            paths.Add(new Path(pathPoints.ToArray(), path.Color));
        }
        var field = new Field(tiles, paths.ToArray());

        Dictionary<string, EnemyType> enemyTypes = new();
        foreach (var enemyType in config.EnemyTypes)
        {
            enemyTypes.Add(enemyType.Id, enemyType);
        }

        List<Wave> waveList = [];
        foreach (var wave in config.Waves)
        {
            waveList.Add(new Wave(wave.Name, wave.Duration, wave.Groups
                .Select(s => new SpawnGroup(enemyTypes[s.Type], s.EnemyCount))
                .ToArray()
            ));
        }
        var waves = waveList.ToArray();

        foreach (var tower in config.Towers)
        {
            PlaceTower(field,
                tower.Position.XPos, tower.Position.YPos,
                config.TowerTypes, tower.Type,
                tower.Level);
        }
        return new Game(field, config.TowerTypes, waves);
    }

    private static void AddPathTile(int x, int y, Tile[,] tiles, List<PathTile> pathPoints)
    {
        var pathTile = new PathTile(x, y);
        tiles[x, y] = pathTile;
        pathPoints.Add(pathTile);
    }

    private static void PlaceTower(Field field, int x, int y, TowerType[] types, int type, int level)
    {
        if (field.Tiles[x, y] is TowerTile towerTile)
        {
            towerTile.Tower = new Tower(
                types.First(t => t.Id == type),
                level,
                new TilePosition(x, y));
        }
    }
}