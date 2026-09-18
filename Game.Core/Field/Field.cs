namespace Game.Core.Field;

using Domain.Field;
using Domain.Field.Tiles;
using Tiles;

public class Field(Tile[,] tiles, Path[] paths) : Domain.Field.Field(tiles, paths)
{
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