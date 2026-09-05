namespace Game.Field.Tiles;

public abstract class Tile(int x, int y)
{
    public int XPosition { get; } = x;
    public int YPosition { get; } = y;
}