namespace Game.Console.Handler;

using Core;
using Entities.Error;

public class ErrorHandler(Game game)
{
    public void HandleError(GameError error, TimeSpan duration)
    {
        game.Error = error;
        game.ErrorExpiresAt = DateTime.UtcNow + duration;
    }
    public void ClearError() { game.Error = null; }
}