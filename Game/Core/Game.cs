namespace Game.Core;

using Entities;
using Entities.Enemy;
using Field;
using Waves;

public class Game
{
    private const int TickRate = 20;
    private Field field;
    private Wave[] waves;
    private List<Enemy> enemies = [];

    public Game(Field field,  Wave[] waves)
    {
        this.field = field;
        this.waves = waves;
    }

    public void Start()
    {
        Console.WriteLine("Starting Game...");
        field.Print();
        foreach (Wave wave in waves)
        {
            while (wave.duration > wave.timer || enemies.Count > 0)
            {
                Update(wave);
                Thread.Sleep(1000 / TickRate);
            }
        }
        Console.WriteLine("Game won!");
    }

    private void Update(Wave wave)
    {
        float deltaTime = (float)1 / TickRate;
        List<SpawnRequest> requests = wave.Update(deltaTime);
        foreach (SpawnRequest request in requests)
        {
            for (int i = 0; i < request.count; i++)
            {
                Path path = field.paths[Random.Shared.Next(0, field.paths.Length)];
                Enemy enemy = new Enemy(request.type, path);
                enemy.OnDefeat += OnEnemyDefeat;
                enemy.OnReach += OnEnemyHit;
                enemies.Add(enemy);
            }
        }

        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies.ToList())
            {
                enemy.Update(deltaTime);
            }

            List<Tower> towers = field.GetTowerTiles()
                .Select(t => t.tower)
                .OfType<Tower>()
                .ToList();
            foreach (Tower tower in towers.ToList())
            {
                tower.Update(deltaTime, enemies);
            }
        }
    }

    private void OnEnemyDefeat(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    private static void OnEnemyHit(Enemy enemy)
    {
        Console.WriteLine("Defeat! Enemy reached tower!");
        Environment.Exit(0);
    }
}