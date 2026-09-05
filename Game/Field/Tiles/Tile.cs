namespace Game.Field.Tiles;

public abstract class Tile
{
    public int xPosition { get; }
    public int yPosition { get; }
    public Position position => new Position(xPosition, yPosition);

    public Tile(int x, int y)
    {
        xPosition = x;
        yPosition = y;
    }
}