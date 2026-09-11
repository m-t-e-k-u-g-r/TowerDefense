namespace Game.Core.Field;

using System.ComponentModel.DataAnnotations;

public class Position(double x, double y)
{
    [Range(0, int.MaxValue)]
    public double XPos { get; } = x;
    [Range(0, int.MaxValue)]
    public double YPos { get; } = y;
}