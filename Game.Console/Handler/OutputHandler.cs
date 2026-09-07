using Game.Entities.Error;

namespace Game.Console.Handler;

using Core;
using Models;
using System;

public class OutputHandler
{
    public void Render(Game game, InputState inputState)
    {
        Console.Clear();

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