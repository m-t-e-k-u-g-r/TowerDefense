namespace Game.Persistence.Entities;

using NpgsqlTypes;

public class Path
{
    public int Id { get; init; }

    public int FieldId { get; init; }

    public string Color { get; init; }

    public NpgsqlPath EnemyPath { get; init; }
}
