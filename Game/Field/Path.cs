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
}