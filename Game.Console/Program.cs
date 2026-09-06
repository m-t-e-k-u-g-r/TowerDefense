namespace Game.Console;

using Handler;

public static class Program
{
    public static void Main(string[] args)
    {
        var game = GameSetup.CreateComplexGame();
        var errorHandler = new ErrorHandler(game);
        var inputHandler = new InputHandler(game, errorHandler);
        var outputHandler = new OutputHandler();
        var runner = new GameRunner();
        runner.Run(game, inputHandler, outputHandler, errorHandler);
    }
}