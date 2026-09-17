namespace Game.Persistence.Entities;

public class GameConfig
{
    public Guid? Id { get; init; }

    public int? Width { get; init; }

    public int? Height { get; init; }

    public string? Paths { get; init; }
}
