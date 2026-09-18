namespace Game.Domain.Field;

public class TilePosition(int x, int y)
{
    public int XPos { get; set; } = x;
    public int YPos { get; set; } = y;
}