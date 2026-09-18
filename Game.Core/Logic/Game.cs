namespace Game.Core.Logic;

using Domain.Entities.Tower;
using Domain.Field;
using Entities;
using Entities.Enemy;
using Entities.Error;
using Entities.Tower;
using Field.Tiles;
using Waves;

public class Game(Core.Field.Field field, TowerType[] towerTypes, Wave[] waves)
{
    public float Damage;
    private readonly List<Enemy> _enemies = [];
    public List<Enemy> Enemies => _enemies;
    public DateTime ErrorExpiresAt;
    public GameError? Error { get; set; }
    public readonly Core.Field.Field Field = field;
    public string GameInfo { get; set; } = "";
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

    public void Update(Wave wave)
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
            .OfType<RegularTower>()
            .ToList();
        foreach (var tower in towers.ToList())
        {
            tower.Update(deltaTime, _enemies);
        }
    }
    public Result<RegularTower> BuyTower(int id, TilePosition position)
    {
        var towerType = GetTowerType(id);
        if (towerType == null) return new Result<RegularTower>(null, new GameError(GameErrorCode.InvalidTowerType, "Tower type not found"));

        var tile = Field.GetTowerTiles()
            .FirstOrDefault(t =>
                t.Tower == null &&
                t.Position.XPos == position.XPos &&
                t.Position.YPos == position.YPos);
        if (tile == null) return new Result<RegularTower>(null, new GameError(GameErrorCode.InvalidPosition, "Position is invalid or occupied"));

        var level = towerType.Levels[0];
        if (_gold < level.Cost) return new Result<RegularTower>(null, new GameError(GameErrorCode.InsufficientGold, "Not enough gold"));

        var newTower = new RegularTower(towerType, 1, position);
        tile.Tower = newTower;

        _gold -= towerType.Levels[0].Cost;
        return new Result<RegularTower>(newTower, null);
    }

    public Result<RegularTower> UpgradeTower(TowerTile tile)
    {
        if (tile.Tower == null) return new Result<RegularTower>(null, new GameError(GameErrorCode.InvalidPosition, "Tower not found"));

        var tower = tile.Tower;
        if (tower is RegularTower regularTower)
        {
            var nextLevel = regularTower.NextLevel;
            if (nextLevel == null) return new Result<RegularTower>(null, new GameError(GameErrorCode.MaxLevelReached, "Tower is already at maximum level"));

            if (_gold < nextLevel.Cost) return new Result<RegularTower>(null, new GameError(GameErrorCode.InsufficientGold, "Not enough gold"));
            _gold -= nextLevel.Cost;
            regularTower.Upgrade();
            return new Result<RegularTower>(regularTower, null);
        }
        return new Result<RegularTower>(null, new GameError(
                GameErrorCode.InvalidPosition, "End tower cannot be upgraded"));
    }

    private void OnEnemyDefeat(Enemy enemy)
    {
        _gold += enemy.Type.Reward;
        _enemies.Remove(enemy);
        Kills++;
    }

    private void OnEnemyDamage(float damage) => Damage += damage;

    private static void OnEnemyHit(Enemy enemy)
    {
        Console.WriteLine("Defeat! Enemy reached tower!");
        Environment.Exit(0);
    }
}