namespace Game.Core;

using Entities;
using Entities.Enemy;
using Field;
using Waves;

public class Game(Field field,  Wave[] waves)
{
    private const int TickRate = 20;
    private readonly List<Enemy> _enemies = [];

    public void Start()
    {
        Console.WriteLine("Starting Game...");
        foreach (var wave in waves)
        {
            while (wave.Duration > wave.Timer || _enemies.Count > 0)
            {
                Update(wave);
                Thread.Sleep(1000 / TickRate);
            }
        }
        Console.WriteLine("Game won!");
    }

    private void Update(Wave wave)
    {
        const float deltaTime = (float)1 / TickRate;
        var requests = wave.Update(deltaTime);
        foreach (var request in requests)
        {
            for (var i = 0; i < request.Count; i++)
            {
                var path = field.Paths[Random.Shared.Next(0, field.Paths.Length)];
                var enemy = new Enemy(request.Type, path);
                enemy.OnDefeat += OnEnemyDefeat;
                enemy.OnReach += OnEnemyHit;
                _enemies.Add(enemy);
            }
        }

        if (_enemies.Count <= 0) {}
        foreach (var enemy in _enemies.ToList())
        {
            enemy.Update(deltaTime);
        }

        var towers = field.GetTowerTiles()
            .Select(t => t.Tower)
            .OfType<Tower>()
            .ToList();
        foreach (var tower in towers.ToList())
        {
            tower.Update(deltaTime, _enemies);
        }
    }

    private void OnEnemyDefeat(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private static void OnEnemyHit(Enemy enemy)
    {
        Console.WriteLine("Defeat! Enemy reached tower!");
        Environment.Exit(0);
    }
}