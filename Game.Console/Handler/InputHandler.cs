namespace Game.Console.Handler;

using Core.Entities;
using Core.Entities.Error;
using Core.Entities.Tower;
using Core.Field;
using Core.Field.Tiles;
using Core.Logic;
using Models;
using System;

public class InputHandler(Game game, ErrorHandler errorHandler)
{
    private readonly InputState _inputState = new();
    public InputState InputState => _inputState;

    public bool HandleInput()
    {
        if (!Console.KeyAvailable) return false;
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
                case ConsoleKey.F:
                    _inputState.Sleep = !_inputState.Sleep;
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.V:
                    _inputState.View = _inputState.View.Next();
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (_inputState.TowerPosition.YPos > 0)
                        _inputState.TowerPosition.YPos--;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (_inputState.TowerPosition.YPos < game.Field.Height - 1)
                        _inputState.TowerPosition.YPos++;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    if (_inputState.TowerPosition.XPos > 0)
                        _inputState.TowerPosition.XPos--;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    if (_inputState.TowerPosition.XPos < game.Field.Width - 1)
                        _inputState.TowerPosition.XPos++;
                    break;
                case ConsoleKey.Enter:
                    HandleTowerAction(_inputState);
                    break;
            }
        }
        return true;
    }

    private void HandleTowerAction(InputState inputState)
    {
        var position = inputState.TowerPosition;
        var tile = game.Field.Tiles[position.XPos, position.YPos];
        if (tile is TowerTile towerTile)
        {
            var result = towerTile switch
            {
                { Tower: null } => TryPlacingTower(inputState.TowerId, position),
                { Tower: not null }=> game.UpgradeTower(towerTile),
            };
            if (result.Error != null)
            {
                errorHandler.HandleError(result.Error, TimeSpan.FromSeconds(3));
            }
            return;
        }

        errorHandler.HandleError(
            new GameError(GameErrorCode.InvalidPosition, "Cannot place tower at path position"),
            TimeSpan.FromSeconds(3)
        );
    }

    private Result<Tower> TryPlacingTower(int? towerId, TilePosition position)
    {
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