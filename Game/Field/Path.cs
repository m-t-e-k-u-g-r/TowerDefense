using Game.Entities;

namespace Game.Field;

using Tiles;

public class Path(PathTile[] pathTiles)
{
    public PathTile[] Tiles { get; } = pathTiles;

    public float GetPathProgress(int pathIndex)
    {
        return (float)pathIndex / Tiles.Length;
    }

    public Position GetPosition(int pathIndex, float progress)
    {
        var tile = Tiles[pathIndex];
        if (pathIndex >= Tiles.Length - 1)
        {
            return new Position(tile.XPosition, tile.YPosition);
        }
        var nextTile = Tiles[pathIndex + 1];
        float x;
        float y;
        if (tile.XPosition == nextTile.XPosition)
        {
            x = tile.XPosition;
            y = tile.YPosition + progress * (nextTile.YPosition - tile.YPosition);
        }
        else
        {
            y = tile.YPosition;
            x = tile.XPosition + progress * (nextTile.XPosition - tile.XPosition);
        }
        return new Position(x, y);
    }
}