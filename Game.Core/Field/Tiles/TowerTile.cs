namespace Game.Core.Field.Tiles;

using Entities.Tower;

public class TowerTile(int x, int y) : Tile(x, y)
{
    public Tower? Tower { get; set; }
}