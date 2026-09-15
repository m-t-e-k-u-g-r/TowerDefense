namespace Game.Core.Field.Tiles;

using Entities.Tower;

public class TowerTile(int x, int y, PlacedTower? tower) : Tile(x, y)
{
    public PlacedTower? Tower { get; set; } = tower;
}