namespace Game.Domain.Entities.Tower;

public class TowerType(int id, string name, TowerLevel[] levels)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public TowerLevel[] Levels => levels;

    public TowerLevel? GetLevel(int level)
    {
        return level < 1 || level > Levels.Length ? null : Levels[level - 1];
    }
}