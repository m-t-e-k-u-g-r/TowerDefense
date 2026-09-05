namespace Game.Field.Tiles;

using Entities;

public class TowerTile : Tile
{
    public Tower? tower { get; set; }

    public TowerTile(int x, int y) : base(x, y)
    {
    }
}