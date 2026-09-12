namespace Game.Console;

using Core.Logic;
using Handler;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var game = CreateGame(args[0]);
            RunGame(game);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Configuration error: {ex.Message}");
        }
    }

    private static Game CreateGame(string path)
    {
        var configLoader = new ConfigLoader();
        var result = configLoader.LoadConfig(path);
        var config = result.Value;

        if (!result.IsSuccess || config == null)
        {
            System.Console.WriteLine($"Error: {result.Error}");
            throw new Exception("Error loading config");
        }

        var setup = new GameSetup();
        return setup.CreateGameFromConfig(config);
    }

    private static void RunGame(Game game)
    {
        var errorHandler = new ErrorHandler(game);
        var inputHandler = new InputHandler(game, errorHandler);
        var outputHandler = new OutputHandler();
        var runner = new GameRunner();
        runner.Run(game, inputHandler, outputHandler, errorHandler);
    }
}