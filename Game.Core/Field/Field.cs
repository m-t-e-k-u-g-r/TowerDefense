using Game.Field.Tiles;

namespace Game.Field;

public class Field(Tile[,] tiles, Path[] paths)
{
    public Path[] Paths { get; } = paths;
    public Tile[,] Tiles { get; } = tiles;
    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);

    public List<TowerTile> GetTowerTiles()
    {
        var towerTiles = new List<TowerTile>();
        foreach (var tile in Tiles)
        {
            if (tile.GetType() == typeof(TowerTile))
            {
                towerTiles.Add((TowerTile)tile);
            }
        }
        return towerTiles;
    }
}