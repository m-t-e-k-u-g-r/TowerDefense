namespace Game.Core.Entities.Error;

public record GameError(GameErrorCode Code, string Message);