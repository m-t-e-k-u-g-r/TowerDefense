namespace Game.Domain.Field;

using Tiles;

public class Field(Tile[,] tiles, Path[] paths)
{
    public Path[] Paths { get; } = paths;
    public Tile[,] Tiles { get; } = tiles;
    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);
}