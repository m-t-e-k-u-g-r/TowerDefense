namespace Game.Console.Models;

using Field;

public class InputState
{
    public Mode? Mode { get; set; }
    public int? TowerId { get; set; }
    public TilePosition TowerPosition { get; } = new(0, 0);
}