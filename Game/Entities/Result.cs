using Game.Entities.Error;

namespace Game.Entities;

public record Result<T>(T? Value, GameError? Error)
{
    public bool IsSuccess => Error is null;
}