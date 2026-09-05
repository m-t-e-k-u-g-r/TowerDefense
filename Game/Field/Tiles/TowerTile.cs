namespace Game.Field.Tiles;

using Entities;

public class TowerTile(int x, int y) : Tile(x, y)
{
    public Tower? Tower { get; set; }
}