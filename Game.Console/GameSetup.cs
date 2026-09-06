using Game.Console.Models.Config;

namespace Game.Console;

using Core;
using Entities.Enemy;
using Entities.Tower;
using Field;
using Field.Tiles;
using Waves;

public class GameSetup
{
    public static Game CreateComplexGame()
    {
        const int size = 20;
        var tiles = new Tile[size, size];
        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                tiles[x, y] = new TowerTile(x, y);
            }
        }

        List<PathTile> pathPoints = new();
        // Create a S-shape path
        for (var y = 0; y < 15; y++) AddPathTile(5, y, tiles, pathPoints);
        for (var x = 6; x < 15; x++) AddPathTile(x, 14, tiles, pathPoints);
        for (var y = 13; y >= 5; y--) AddPathTile(15, y, tiles, pathPoints);
        for (var x = 16; x < 19; x++) AddPathTile(x, 5, tiles, pathPoints);

        var path = new Path(pathPoints.ToArray());
        var field = new Field(tiles, [path]);

        // Enemy types
        var scout   = new EnemyType("scout", 30,   2.0f,  0.20f,  8);
        var soldier = new EnemyType("soldier", 100,  1.0f,  0.05f,  15);
        var tank    = new EnemyType("tank", 500,  0.5f,  0.00f,  40);

        var brute   = new EnemyType("brute", 1000, 0.7f,  0.10f,  70);
        var runner  = new EnemyType("runner", 60,   2.8f,  0.30f,  20);
        var elite   = new EnemyType("elite", 250,  1.3f,  0.15f,  35);
        var boss    = new EnemyType("boss", 3000, 0.35f, 0.05f, 250);

        // Waves
        var wave1 = new Wave("Wave 1", 20, [
            new SpawnGroup(scout, 20)
        ]);
        var wave2 = new Wave("Wave 2", 30, [
            new SpawnGroup(scout, 15),
            new SpawnGroup(soldier, 20)
        ]);
        var wave3 = new Wave("Wave 3", 45, [
            new SpawnGroup(scout, 20),
            new SpawnGroup(soldier, 30),
            new SpawnGroup(tank, 5)
        ]);
        var wave4 = new Wave("Wave 4", 55, [
            new SpawnGroup(runner, 25),
            new SpawnGroup(soldier, 35),
            new SpawnGroup(tank, 8)
        ]);
        var wave5 = new Wave("Wave 5", 65, [
            new SpawnGroup(scout, 30),
            new SpawnGroup(soldier, 40),
            new SpawnGroup(elite, 10),
            new SpawnGroup(tank, 10)
        ]);
        var wave6 = new Wave("Wave 6", 75, [
            new SpawnGroup(runner, 35),
            new SpawnGroup(soldier, 50),
            new SpawnGroup(elite, 15),
            new SpawnGroup(brute, 5)
        ]);
        var wave7 = new Wave("Wave 7", 90, [
            new SpawnGroup(scout, 40),
            new SpawnGroup(runner, 40),
            new SpawnGroup(soldier, 60),
            new SpawnGroup(tank, 15),
            new SpawnGroup(elite, 20)
        ]);
        var wave8 = new Wave("Wave 8", 105, [
            new SpawnGroup(runner, 50),
            new SpawnGroup(soldier, 70),
            new SpawnGroup(tank, 20),
            new SpawnGroup(brute, 10)
        ]);
        var wave9 = new Wave("Wave 9", 120, [
            new SpawnGroup(scout, 60),
            new SpawnGroup(runner, 50),
            new SpawnGroup(elite, 30),
            new SpawnGroup(brute, 15),
            new SpawnGroup(tank, 25)
        ]);
        var wave10 = new Wave("Final Wave", 150, [
            new SpawnGroup(soldier, 100),
            new SpawnGroup(runner, 60),
            new SpawnGroup(elite, 40),
            new SpawnGroup(tank, 30),
            new SpawnGroup(brute, 20),
            new SpawnGroup(boss, 1)
        ]);
        Wave[] waves = [
            wave1, wave2, wave3, wave4, wave5,
            wave6, wave7, wave8, wave9, wave10
        ];
        
        var basicTower = new TowerType(1, "Basic Tower",
        [
            new TowerLevel(cost: 100, damage: 25, range: 7, fireRate: 2),
            new TowerLevel(cost: 150, damage: 35, range: 7, fireRate: 2.2f),
            new TowerLevel(cost: 225, damage: 50, range: 8, fireRate: 2.4f),
        ]);

        var rapidTower = new TowerType(2, "Rapid Tower",
        [
            new TowerLevel(cost: 125, damage: 30, range: 7, fireRate: 3),
            new TowerLevel(cost: 175, damage: 40, range: 7, fireRate: 3.5f),
            new TowerLevel(cost: 250, damage: 55, range: 8, fireRate: 4),
        ]);

        var longRangeTower = new TowerType(3, "Long Range Tower",
        [
            new TowerLevel(cost: 150, damage: 25, range: 9, fireRate: 4),
            new TowerLevel(cost: 225, damage: 35, range: 10, fireRate: 4.5f),
            new TowerLevel(cost: 325, damage: 50, range: 11, fireRate: 5),
        ]);

        var heavyTower = new TowerType(4, "Heavy Tower",
        [
            new TowerLevel(cost: 200, damage: 45, range: 6, fireRate: 5),
            new TowerLevel(cost: 300, damage: 60, range: 6, fireRate: 5.5f),
            new TowerLevel(cost: 450, damage: 80, range: 7, fireRate: 6),
        ]);

        var sniperTower = new TowerType(5, "Sniper Tower",
        [
            new TowerLevel(cost: 250, damage: 60, range: 5, fireRate: 4),
            new TowerLevel(cost: 375, damage: 85, range: 6, fireRate: 4.5f),
            new TowerLevel(cost: 550, damage: 120, range: 7, fireRate: 5),
        ]);
        TowerType[] towerTypes = [basicTower, rapidTower, longRangeTower, heavyTower, sniperTower];

        // Towers
        PlaceTower(field, 4, 2, basicTower, 1);
        PlaceTower(field, 6, 2, basicTower, 2);

        PlaceTower(field, 3, 7, rapidTower, 1);
        PlaceTower(field, 7, 8, rapidTower, 2);

        PlaceTower(field, 10, 15, heavyTower, 1);
        PlaceTower(field, 12, 12, longRangeTower, 1);

        PlaceTower(field, 14, 10, longRangeTower, 1);
        PlaceTower(field, 16, 12, longRangeTower, 2);

        PlaceTower(field, 17, 6, sniperTower, 1);
        PlaceTower(field, 19, 5, longRangeTower, 2);
        PlaceTower(field, 18, 8, heavyTower, 1);

        // Additional late-game towers
        PlaceTower(field, 10, 10, heavyTower, 2);
        PlaceTower(field, 13, 7, longRangeTower, 2);
        PlaceTower(field, 8, 13, heavyTower, 2);

        return new Game(field, towerTypes, waves);
    }

    private static void AddPathTile(int x, int y, Tile[,] tiles, List<PathTile> pathPoints)
    {
        var pathTile = new PathTile(x, y);
        tiles[x, y] = pathTile;
        pathPoints.Add(pathTile);
    }

    private static void PlaceTower(Field field, int x, int y, TowerType type, int level)
    {
        if (field.Tiles[x, y] is TowerTile towerTile)
        {
            towerTile.Tower = new Tower(type, level, new TilePosition(x, y));
        }
    }
}