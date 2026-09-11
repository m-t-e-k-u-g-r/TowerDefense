namespace Game.Core.Field;

using Tiles;

public class Path(PathTile[] pathTiles, ConsoleColor color)
{
    public PathTile[] Tiles { get; } = pathTiles;
    public ConsoleColor Color { get; set; } = color;

    public float GetPathProgress(int pathIndex)
    {
        return (float)pathIndex / Tiles.Length;
    }

    public Position GetPosition(int pathIndex, float progress)
    {
        var tile = Tiles[pathIndex];
        if (pathIndex >= Tiles.Length - 1)
        {
            return new Position(tile.Position.XPos, tile.Position.YPos);
        }
        var nextTile = Tiles[pathIndex + 1];
        float x;
        float y;
        if (tile.Position.XPos == nextTile.Position.XPos)
        {
            x = tile.Position.XPos;
            y = tile.Position.YPos + progress * (nextTile.Position.YPos - tile.Position.YPos);
        }
        else
        {
            y = tile.Position.YPos;
            x = tile.Position.XPos + progress * (nextTile.Position.XPos - tile.Position.XPos);
        }
        return new Position(x, y);
    }

    public bool IsPartOfPath(TilePosition position)
    {
        return Tiles.Any(tile => tile.Position == position);
    }
}