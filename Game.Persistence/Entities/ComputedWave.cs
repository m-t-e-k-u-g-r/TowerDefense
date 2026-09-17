namespace Game.Persistence.Entities;

public class ComputedWave
{
    public int Id { get; init; }

    public Guid GameId { get; init; }

    public int Index { get; init; }

    public string Name { get; init; }

    public short Duration { get; init; }

    public string SpawnGroups { get; init; }
}
