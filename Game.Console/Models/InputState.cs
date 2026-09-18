namespace Game.Console.Models;

using Domain.Field;

public class InputState
{
    public View View { get; set; } = View.Stats;
    public int? TowerId { get; set; }
    public bool Sleep { get; set; } = true;
    public TilePosition TowerPosition { get; } = new(0, 0);
}