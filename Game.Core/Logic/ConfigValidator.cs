namespace Game.Core.Logic;

using Entities.Config;
using Entities.Enemy;
using Entities.Tower;
using System;

public static class ConfigValidator
{
    public static bool CheckConfiguration(GameConfig config)
    {
        var width = config.Field.Width;
        var height = config.Field.Height;
        return FieldSizeIsOk(width, height) &&
               PathsExist(config.Paths) &&
               WavesExist(config.Waves) &&
               EnemyTypesExist(config.EnemyTypes) &&
               TowerTypesExist(config.TowerTypes) &&
               WaveDurationIsOk(config.Waves) &&
               EnemyTypesAreOk(config.EnemyTypes) &&
               TowerTypeLevelsExist(config.TowerTypes) &&
               TowerPositionsAreValid(config.Towers, width, height) &&
               TowerTypesAreValid(config.TowerTypes) &&
               SpawnWavesAreNonZero(config.Waves) &&
               NoDuplicateEnemyType(config.EnemyTypes) &&
               PathsAreValid(config.Paths, width, height) &&
               ReferencedEnemiesAreSet(
                   config.Waves, config.EnemyTypes.Select(et => et.Id).ToList()
                   ) &&
               ReferencedTowersAreSet(config.Towers, config.TowerTypes) &&
               NoDuplicatedOccupiedTowerTiles(config.Towers);
    }

    public static bool FieldSizeIsOk(int width, int height)
    {
        if (width > 0 && height > 0) return true;
        Console.WriteLine("Invalid field size provided");
        return false;
    }
    public static bool PathsExist(ConfigPath[] paths)
    {
        if (paths.Length >= 1) return true;
        Console.WriteLine("No paths found in configuration");
        return false;
    }
    public static bool WavesExist(ConfigWave[] waves)
    {
        if (waves.Length > 0) return true;
        Console.WriteLine("No waves found in configuration");
        return false;
    }
    public static bool EnemyTypesExist(EnemyType[] enemyTypes)
    {
        if (enemyTypes.Length > 0) return true;
        Console.WriteLine("No enemy types found in configuration");
        return false;
    }
    public static bool TowerTypesExist(TowerType[] towerTypes)
    {
        if (towerTypes.Length > 0) return true;
        Console.WriteLine("No tower types found in configuration");
        return false;
    }
    public static bool WaveDurationIsOk(ConfigWave[] waves)
    {
        if (waves.All(w => w.Duration >= 1)) return true;
        Console.WriteLine("Wave duration may not be less than 1 second");
        return false;
    }
    public static bool EnemyTypesAreOk(EnemyType[] enemyTypes)
    {
        if (enemyTypes.All(e =>
                e is
                    {
                        MaxHealth: > 0,
                        MoveSpeed: > 0,
                        Evasion: >= 0 and < 1
                    })
            ) return true;
        Console.WriteLine("Invalid enemy type provided");
        return false;
    }
    public static bool TowerTypeLevelsExist(TowerType[] towerTypes)
    {
        if (towerTypes.All(t => t.Levels.Length != 0)) return true;
        Console.WriteLine("Tower type has no levels");
        return false;
    }
    public static bool TowerPositionsAreValid(ConfigTower[] towers, int width, int height)
    {
        if (towers.All(t =>
                t.Position.XPos >= 0 &&
                t.Position.XPos < width &&
                t.Position.YPos >= 0 &&
                t.Position.YPos < height)) return true;
        Console.WriteLine("Tower position outside field");
        return false;
    }
    public static bool TowerTypesAreValid(TowerType[] towerTypes)
    {
        if (towerTypes.All(t =>
                t.Levels.All(l =>
                    l is
                        {
                            Cost: >= 1,
                            Damage: >= 1,
                            Range: >= 1,
                            FireRate: > 0
                        })
                )
            ) return true;
        Console.WriteLine("Invalid tower type provided. Cost, damage, range and fire rate may not be 0");
        return false;
    }
    public static bool SpawnWavesAreNonZero(ConfigWave[] waves)
    {
        if (waves.SelectMany(wave => wave.Groups).All(group => group.EnemyCount >= 1)) return true;
        Console.WriteLine("Enemy count of spawn group may not be less than 1");
        return false;
    }
    public static bool NoDuplicateEnemyType(EnemyType[] enemyTypes)
    {
        var enemyIds = new HashSet<string>();
        foreach (var enemyType in enemyTypes)
        {
            if (!enemyIds.Add(enemyType.Id))
            {
                Console.WriteLine("Duplicate enemy type: {0}", enemyType.Id);
                return false;
            }
        }
        return true;
    }
    public static bool PathsAreValid(ConfigPath[] paths, int width, int height)
    {
        if (paths.Any(path =>
                !(path.Tiles.Length > 0 &&
                  path.Tiles.All(tile =>
                      tile.Length == 2 &&
                      tile[0] < width && tile[0] >= 0 &&
                      tile[1] < height && tile[1] >= 0) &&
                  path.Tiles.Select(tile => (tile[0], tile[1])).Distinct().Count() == path.Tiles.Length)))
        {
            Console.WriteLine("Invalid path provided");
            return false;
        }
        return true;
    }
    public static bool ReferencedEnemiesAreSet(ConfigWave[] waves, List<string> enemyIds)
    {
        var invalidEnemy = waves
            .SelectMany(wave => wave.Groups)
            .Select(group => group.Type)
            .FirstOrDefault(id => !enemyIds.Contains(id));
        if (invalidEnemy == null) return true;
        Console.WriteLine("Undefined enemy {0} found", invalidEnemy);
        return false;
    }
    public static bool ReferencedTowersAreSet(ConfigTower[] towers, TowerType[] towerTypes)
    {
        if (towers.All(ct =>
            {
                var towerType = towerTypes.FirstOrDefault(t => t.Id == ct.Type);
                return towerType != null && ct.Level >= 1 && ct.Level < towerType.Levels.Length;
            })) return true;
        Console.WriteLine("Invalid tower type or level provided");
        return false;
    }
    public static bool NoDuplicatedOccupiedTowerTiles(ConfigTower[] towers)
    {
        if (towers
                .Select(t => (t.Position.XPos, t.Position.YPos))
                .Distinct()
                .Count() != towers.Length)
        {
            Console.WriteLine("Multiple towers occupy the same position");
            return false;
        }
        return true;
    }
}