using Game.Field.Tiles;

namespace Game.Field;

public class Field
{
    private Tile[,] tiles { get; }
    public Path[] paths { get; }
    public Tile[,] Tiles => tiles;

    public Field(Tile[,] tiles, Path[] paths)
    {
        this.tiles = tiles;
        this.paths = paths;
    }

    public List<TowerTile> GetTowerTiles()
    {
        List<TowerTile> towerTiles = new List<TowerTile>();
        foreach (Tile tile in tiles)
        {
            if (tile.GetType() == typeof(TowerTile))
            {
                towerTiles.Add((TowerTile)tile);
            }
        }
        return towerTiles;
    }

    public void Print()
    {
        for (int row = 0; row < tiles.GetLength(0); row++)
        {
            for (int col = 0; col < tiles.GetLength(1); col++)
            {
                if (tiles[row, col].GetType() == typeof(PathTile))
                {
                    Console.Write("[P]");
                }
                else
                {
                    if (tiles[row, col] is TowerTile tile)
                    {
                        if (tile.tower != null)
                        {
                            Console.Write("[T]");
                        }
                        else
                        {
                            Console.Write("[ ]");
                        }
                    }
                }
            }
            Console.WriteLine();
        }
    }
}