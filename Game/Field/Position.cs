namespace Game.Field;

public class Position
{
    public double xPos { get; }
    public double yPos { get; }

    public Position(double x, double y)
    {
        this.xPos = x;
        this.yPos = y;
    }
}