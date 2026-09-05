using Game.Entities;

namespace Game.Field;

using Tiles;

public class Path
{
    public PathTile[] tiles { get; }

    public Path(PathTile[] tiles)
    {
        this.tiles = tiles;
    }

    public float GetPathProgress(int pathIndex)
    {
        return (float)pathIndex / tiles.Length;
    }

    public Position GetPosition(int pathIndex, float progress)
    {
        PathTile tile = tiles[pathIndex];
        if (pathIndex >= tiles.Length - 1)
        {
            return new Position(tile.xPosition, tile.yPosition);
        }
        PathTile nextTile = tiles[pathIndex + 1];
        float x;
        float y;
        if (tile.xPosition == nextTile.xPosition)
        {
            x = tile.xPosition;
            y = tile.yPosition + progress * (nextTile.yPosition - tile.yPosition);
        }
        else
        {
            y = tile.yPosition;
            x = tile.xPosition + progress * (nextTile.xPosition - tile.xPosition);
        }
        return new Position(x, y);
    }
}