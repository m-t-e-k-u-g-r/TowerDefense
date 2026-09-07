namespace Game.Console.Models;

public enum View
{
    Stats,
    Board
}

public static class ViewExtension
{
    public static View Next(this View view)
    {
        return view switch
        {
            View.Board => View.Stats,
            View.Stats => View.Board,
            _ => View.Stats,
        };
    }
}