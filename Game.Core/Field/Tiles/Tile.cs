namespace Game.Field.Tiles;

public abstract class Tile(int x, int y)
{
    public TilePosition Position { get; } = new(x, y);
}