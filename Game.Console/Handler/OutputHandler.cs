namespace Game.Console.Handler;

using Core.Entities.Tower;
using Core.Field;
using Core.Field.Tiles;
using Core.Logic;
using Models;
using System;

public class OutputHandler
{
    public void Render(Game game, InputState inputState)
    {
        var selectedPosition = inputState.TowerPosition;
        var tiles = game.Field.Tiles;
        var selectedTile = tiles[selectedPosition.XPos, selectedPosition.YPos];

        Console.Clear();

        // Header
        Console.WriteLine("========================================");
        Console.WriteLine("              TOWER DEFENSE             ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Status : {(game.Paused ? "Paused" : "Running")}");
        Console.WriteLine();

        // Selection information
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("SELECTED");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(
            $"Position : ({selectedPosition.XPos}, {selectedPosition.YPos})");
        Console.WriteLine($"Tile     : {selectedTile.GetType().Name}");

        if (selectedTile is TowerTile { Tower: var tower})
        {
            switch (tower)
            {
                case RegularTower regular:
                    Console.WriteLine($"Tower    : {regular.Type.Name}");
                    Console.WriteLine($"Level    : {regular.Level}");
                    break;
                case FinalTower:
                    Console.WriteLine("Tower    : Fixed Tower");
                    break;
            }
        }

        var selectedTower = game.TowerTypes
            .FirstOrDefault(t => t.Id == inputState.TowerId);

        Console.WriteLine($"Building : {selectedTower?.Name ?? "None"}");
        Console.WriteLine();

        // Main view
        Console.WriteLine("----------------------------------------");

        switch (inputState.View)
        {
            case View.Stats:
                Console.WriteLine("STATISTICS");
                Console.WriteLine("----------------------------------------");
                ViewStats(game);
                break;

            case View.Board:
                Console.WriteLine("BOARD");
                Console.WriteLine("----------------------------------------");

                ViewBoard(tiles, game.Field.Paths, selectedPosition);

                Console.WriteLine();
                Console.WriteLine("Legend: [P] Path  [T] Tower  [ ] Empty  [X] Selected");
                break;
        }

        // Game information
        if (!string.IsNullOrWhiteSpace(game.GameInfo))
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("INFO");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(game.GameInfo.Trim());
        }

        // Error information
        if (game.Error != null)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("ERROR");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(game.Error.Message);
        }
    }

    private static void ViewBoard(Tile[,] tiles, Path[] paths, TilePosition selectedPosition)
    {
        for (var y = 0; y < tiles.GetLength(1); y++)
        {
            for (var x = 0; x < tiles.GetLength(0); x++)
            {
                if (selectedPosition.XPos == x &&
                    selectedPosition.YPos == y)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[X]");
                    Console.ResetColor();
                    continue;
                }

                var tile = tiles[x, y];

                switch (tile)
                {
                    case PathTile pathTile:
                    {
                        var path = paths.First(p =>
                            p.IsPartOfPath(pathTile.Position));

                        Console.ForegroundColor = path.Color;
                        Console.Write("[P]");
                        Console.ResetColor();
                        break;
                    }

                    case TowerTile { Tower: null }:
                        Console.Write("[ ]");
                        break;

                    case TowerTile:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("[T]");
                        Console.ResetColor();
                        break;
                }
            }
            Console.WriteLine();
        }
    }

    private static void ViewStats(Game game)
    {
        Console.WriteLine($"Wave       : {game.Wave?.Name ?? "None"}");
        Console.WriteLine($"Gold       : {game.Gold}");
        Console.WriteLine($"Spawns     : {game.Spawns}");
        Console.WriteLine($"Kills      : {game.Kills}");
        Console.WriteLine($"Damage     : {game.Damage}");
        Console.WriteLine($"Runtime    : {game.RuntimeMs / 1000}s");

        var damagePerSecond = game.RuntimeMs > 0
            ? game.Damage * 1000.0 / game.RuntimeMs
            : 0;

        Console.WriteLine($"Damage/s   : {damagePerSecond:F2}");

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("CONTROLS");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("[F] FastForward  [Arrow/WASD] Move");
        Console.WriteLine("[V] Switch View  [Enter] Confirm");
        Console.WriteLine("[Space] Pause    [Q] Quit");
    }
}