namespace Game.Console.Handler;

using Core;
using Entities;
using Entities.Error;
using Entities.Tower;
using Field;
using Models;
using System;

public class InputHandler(Game game)
{
    private readonly InputState _inputState = new();
    public InputState InputState => _inputState;
    private readonly ErrorHandler _errorHandler = new();

    public void HandleInput()
    {
        if (!Console.KeyAvailable) return;
        var keyInfo = Console.ReadKey(true);
        try
        {
            var digit = TryGetDigit(keyInfo.KeyChar);
            if (game.TowerTypes.Any(t => t.Id == digit))
            { _inputState.TowerId = game.TowerTypes.First(t => t.Id == digit).Id; }
        }
        catch
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Spacebar:
                    game.Paused = !game.Paused;
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.P:
                    _inputState.Mode = Mode.Placement;
                    break;
                case ConsoleKey.U:
                    _inputState.Mode = Mode.Upgrade;
                    break;
                case ConsoleKey.UpArrow:
                    if (_inputState.TowerPosition.YPos > 0)
                        _inputState.TowerPosition.YPos--;
                    break;
                case ConsoleKey.DownArrow:
                    if (_inputState.TowerPosition.YPos < game.Field.Height - 1)
                        _inputState.TowerPosition.YPos++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_inputState.TowerPosition.XPos > 0)
                        _inputState.TowerPosition.XPos--;
                    break;
                case ConsoleKey.RightArrow:
                    if (_inputState.TowerPosition.XPos < game.Field.Width - 1)
                        _inputState.TowerPosition.XPos++;
                    break;
                case ConsoleKey.Enter:
                    if (_inputState.Mode != null) HandleTowerAction(_inputState);
                    break;
            }
        }
    }

    private void HandleTowerAction(InputState inputState)
    {
        var position = inputState.TowerPosition;
        var result = inputState.Mode switch
        {
            Mode.Placement => TryPlacingTower(inputState, position),
            Mode.Upgrade => game.UpgradeTower(position),
            _ => new Result<Tower>(null, new GameError(GameErrorCode.InvalidMode, "Invalid mode selected."))
        };
        var error = result.Error;
        if (result.IsSuccess) { Console.WriteLine("Tower placed"); }
        if (error != null) _errorHandler.HandleError(error);
    }

    private Result<Tower> TryPlacingTower(InputState inputState, TilePosition position)
    {
        var towerId = inputState.TowerId;
        return towerId != null 
            ? game.BuyTower(towerId.Value, position)
            : new Result<Tower>(null, new GameError(GameErrorCode.InvalidTowerType, "Invalid tower type."));
    }

    private int TryGetDigit(char keyChar)
    {
        const string digits = "0123456789";
        return digits.Contains(keyChar)
            ? int.Parse(keyChar.ToString())
            : throw new KeyNotFoundException();
    }
}