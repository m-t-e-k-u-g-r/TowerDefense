namespace Game.Console;

using Core.Logic;
using Handler;
using System;

public class GameRunner
{
    public void Run(Game game, InputHandler inputHandler, OutputHandler outputHandler, ErrorHandler errorHandler)
    {
        Console.WriteLine("Starting Game...");
        foreach (var wave in game.Waves)
        {
            game.Wave = wave;
            Console.WriteLine("{0} starts now!", wave.Name);
            while (wave.Duration > wave.Timer || game.Enemies.Count > 0)
            {
                if (game.Error != null && DateTime.UtcNow >= game.ErrorExpiresAt) { errorHandler.ClearError(); }
                var inputReceived = inputHandler.HandleInput();
                if (!game.Paused)
                {
                    game.Update(wave);
                }
                if (inputReceived || !game.Paused)
                {
                    outputHandler.Render(game, inputHandler.InputState);
                }

                if (inputHandler.InputState.Sleep) Thread.Sleep(1000 / Game.TickRate);
            }
            Console.WriteLine("{0} completed!", wave.Name);
            if (inputHandler.InputState.Sleep) Thread.Sleep(3000);
        }
        Console.WriteLine("Game won!");
    }
}