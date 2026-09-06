namespace Game.Core;

using Entities;
using Entities.Enemy;
using Entities.Error;
using Entities.Tower;
using Field;
using Waves;

public class Game(Field field, TowerType[] towerTypes, Wave[] waves)
{
    public float Damage;
    private readonly List<Enemy> _enemies = [];
    public List<Enemy> Enemies => _enemies;
    public readonly Field Field = field;
    private int _gold;
    public int Gold => _gold;
    public int Kills;
    public bool Paused;
    public int RuntimeMs;
    public int Spawns;
    public const int TickRate = 20;
    public TowerType[] TowerTypes => towerTypes;
    private TowerType? GetTowerType(int id) { return TowerTypes.FirstOrDefault(t => t.Id == id); }
    public Wave[] Waves => waves;
    public Wave? Wave;

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
        RuntimeMs += (int)MathF.Round(deltaTime * 1000);
        var requests = wave.Update(deltaTime);
        foreach (var request in requests)
        {
            for (var i = 0; i < request.Count; i++)
            {
                var path = Field.Paths[Random.Shared.Next(0, Field.Paths.Length)];
                var enemy = new Enemy(request.Type, path);
                enemy.OnDefeat += OnEnemyDefeat;
                enemy.OnHit += OnEnemyDamage;
                enemy.OnReach += OnEnemyHit;
                _enemies.Add(enemy);
            }
            Spawns += request.Count;
        }

        foreach (var enemy in _enemies.ToList()) { enemy.Update(deltaTime); }

        var towers = Field.GetTowerTiles()
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
        _gold += enemy.Type.Reward;
        _enemies.Remove(enemy);
        Kills++;
    }

    private void OnEnemyDamage(float damage) { Damage += damage; }

    private static void OnEnemyHit(Enemy enemy)
    {
        Console.WriteLine("Defeat! Enemy reached tower!");
        Environment.Exit(0);
    }
}