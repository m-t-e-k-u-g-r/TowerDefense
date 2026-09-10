namespace Game.Core.Entities;

using Error;

public record Result<T>(T? Value, GameError? Error)
{
    public bool IsSuccess => Error is null;
}