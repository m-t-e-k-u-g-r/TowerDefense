namespace Game.Console.Handler;

using Entities.Error;
using System;

public class ErrorHandler
{
    public GameError? Error { get; private set; }
    public void HandleError(GameError error)
    {
        Error = error;
        switch (error.Code)
        {
            case GameErrorCode.InsufficientGold:
                Console.WriteLine("Insufficient gold");
                break;
            case GameErrorCode.InvalidMode:
                Console.WriteLine("Invalid mode");
                break;
            case GameErrorCode.InvalidPosition:
                Console.WriteLine("Invalid position");
                break;
            case GameErrorCode.InvalidTowerType:
                Console.WriteLine("Invalid tower type");
                break;
            case GameErrorCode.MaxLevelReached:
                Console.WriteLine("Max level reached");
                break;
            case GameErrorCode.TileOccupied:
                Console.WriteLine("Tile occupied");
                break;
            default:
                Console.WriteLine("Unknown error");
                break;
        }
    }
}