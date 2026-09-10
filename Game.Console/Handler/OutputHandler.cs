namespace Game.Console.Handler;

using Core.Field;
using Core.Field.Tiles;
using Core.Logic;
using Models;
using System;

public class OutputHandler
{
    public void Render(Game game, InputState inputState)
    {
        Console.Clear();
        Console.WriteLine("Running: {0}", !game.Paused);
        switch (inputState.View)
        {
            case View.Stats:
                ViewStats(game, inputState);
                break;
            case View.Board:
                var enemyPositions = game.Enemies
                    .Select(e => e.Position)
                    .ToList();
                ViewBoard(game.Field.Tiles, game.Field.Paths, inputState.TowerPosition, enemyPositions);
                break;
        }
    }

    private static void ViewBoard(Tile[,] tiles, Path[] paths, TilePosition selectedPosition, List<Position> enemyPositions)
    {
        // Display coordinates of selected position
        Console.WriteLine("Selected position: ({0}, {1})", selectedPosition.XPos, selectedPosition.YPos);
        var selectedTile = tiles[selectedPosition.XPos, selectedPosition.YPos];
        var selectedType = selectedTile.GetType().Name;
        // Display tile type of selected position
        Console.WriteLine("Type: {0}", selectedType);
        if (selectedTile is TowerTile tt && tt.Tower != null)
        {
            // Optionally display Tower type and level
            Console.WriteLine("{0} Level: {1}", tt.Tower.Type.Name, tt.Tower.Level);
        };

        // Draw board itself
        for (var y = 0; y < tiles.GetLength(0); y++)
        {
            for (var x = 0; x < tiles.GetLength(1); x++)
            {
                if (selectedPosition.XPos == x && selectedPosition.YPos == y)
                {
                    // Highlight selected position
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[X]");
                    Console.ResetColor();
                    continue;
                }
                var tile = tiles[x, y];
                switch (tile)
                {
                    case PathTile pathTile:
                        // Highlight path tiles with different colors
                        var path = paths.First(p => p.IsPartOfPath(pathTile.Position));
                        Console.ForegroundColor = path.Color;
                        Console.Write("[P]");
                        Console.ResetColor();
                        break;
                    case TowerTile towerTile:
                        if (towerTile.Tower == null)
                        {
                            // Display empty tower tile
                            Console.Write("[ ]");
                            break;
                        }
                        // Display tile with tower
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("{0}", "[T]");
                        Console.ResetColor();
                        break;
                }
            }
            Console.WriteLine();
        }
    }

    private static void ViewStats(Game game, InputState inputState)
    {
        Console.WriteLine("{0}", game.Wave?.Name);
        Console.WriteLine();

        Console.WriteLine("Gold: {0}", game.Gold);
        Console.WriteLine("Total spawns: {0}", game.Spawns);
        Console.WriteLine("Total kills: {0}", game.Kills);
        Console.WriteLine("Total damage: {0}", game.Damage);
        Console.WriteLine("Total runtime: {0}s", game.RuntimeMs / 1000);
        Console.WriteLine("Damage/s: {0:F2}", game.Damage * 1000 / game.RuntimeMs);

        Console.WriteLine();
        Console.WriteLine("Mode: {0}", inputState.Mode);
        Console.WriteLine("Tower: {0}", game.TowerTypes.FirstOrDefault(t => t.Id == inputState.TowerId)?.Name);
        Console.WriteLine("Position: ({0}, {1})", inputState.TowerPosition.XPos, inputState.TowerPosition.YPos);
        Console.WriteLine();

        Console.WriteLine("[P] Place [U] Upgrade [Arrow Keys | WASD] Move");
        Console.WriteLine("[V] Switch View [Enter] Confirm [Space] Pause [Q] Quit");

        if (game.GameInfo.Trim() != string.Empty) { Console.WriteLine("Info: {0}", game.GameInfo.Trim()); }

        if (game.Error != null)
        {
            Console.WriteLine();
            Console.WriteLine(game.Error.Message);
        }
    }
}