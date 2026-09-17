namespace Game.Persistence.Context;

using Entities;
using Microsoft.EntityFrameworkCore;

public partial class TowerDefenseContext : DbContext
{
    public TowerDefenseContext()
    {
    }

    public TowerDefenseContext(DbContextOptions<TowerDefenseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComputedTower> ComputedTowers { get; set; }

    public virtual DbSet<ComputedWave> ComputedWaves { get; set; }

    public virtual DbSet<Enemy> Enemies { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<FinalTower> FinalTowers { get; set; }

    public virtual DbSet<GameConfig> GameConfigs { get; set; }

    public virtual DbSet<GameConfiguration> GameConfigurations { get; set; }

    public virtual DbSet<GameWave> GameWaves { get; set; }

    public virtual DbSet<Path> Paths { get; set; }

    public virtual DbSet<SpawnGroup> SpawnGroups { get; set; }

    public virtual DbSet<TowerLevel> TowerLevels { get; set; }

    public virtual DbSet<TowerType> TowerTypes { get; set; }

    public virtual DbSet<Wave> Waves { get; set; }

    public virtual DbSet<WaveSpawnGroup> WaveSpawnGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComputedTower>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("computed_tower");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Levels)
                .HasColumnType("jsonb")
                .HasColumnName("levels");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ComputedWave>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("computed_wave");

            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.SpawnGroups)
                .HasColumnType("jsonb")
                .HasColumnName("spawn_groups");
        });

        modelBuilder.Entity<Enemy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("enemy_pkey");

            entity.ToTable("enemy");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuidv7()")
                .HasColumnName("id");
            entity.Property(e => e.Evasion)
                .HasPrecision(3, 2)
                .HasColumnName("evasion");
            entity.Property(e => e.MaxHealth).HasColumnName("max_health");
            entity.Property(e => e.MoveSpeed)
                .HasPrecision(3, 2)
                .HasColumnName("move_speed");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Reward).HasColumnName("reward");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("field_pkey");

            entity.ToTable("field");

            entity.HasIndex(e => new { e.Width, e.Height }, "field_width_height_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<FinalTower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("final_tower_pkey");

            entity.ToTable("final_tower");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuidv7()")
                .HasColumnName("id");
            entity.Property(e => e.Damage)
                .HasPrecision(10, 2)
                .HasColumnName("damage");
            entity.Property(e => e.FireRate)
                .HasPrecision(10, 2)
                .HasColumnName("fire_rate");
            entity.Property(e => e.PathId).HasColumnName("path_id");
            entity.Property(e => e.Range)
                .HasPrecision(10, 2)
                .HasColumnName("range");
            entity.Property(e => e.XPos).HasColumnName("x_pos");
            entity.Property(e => e.YPos).HasColumnName("y_pos");
        });

        modelBuilder.Entity<GameConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("game_config");

            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Paths)
                .HasColumnType("jsonb")
                .HasColumnName("paths");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<GameConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("game_configuration_pkey");

            entity.ToTable("game_configuration");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuidv7()")
                .HasColumnName("id");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
        });

        modelBuilder.Entity<GameWave>(entity =>
        {
            entity.HasKey(e => new { e.GameId, e.WaveId }).HasName("game_wave_pkey");

            entity.ToTable("game_wave");

            entity.HasIndex(e => new { e.GameId, e.Index }, "game_wave_game_id_index_key").IsUnique();

            entity.HasIndex(e => e.WaveId, "game_wave_wave_id_key").IsUnique();

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.WaveId).HasColumnName("wave_id");
            entity.Property(e => e.Index).HasColumnName("index");
        });

        modelBuilder.Entity<Path>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("path_pkey");

            entity.ToTable("path");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.EnemyPath).HasColumnName("enemy_path");
            entity.Property(e => e.FieldId).HasColumnName("field_id");
        });

        modelBuilder.Entity<SpawnGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("spawn_group_pkey");

            entity.ToTable("spawn_group");

            entity.HasIndex(e => new { e.EnemyId, e.Count }, "spawn_group_enemy_id_count_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.EnemyId).HasColumnName("enemy_id");
        });

        modelBuilder.Entity<TowerLevel>(entity =>
        {
            entity.HasKey(e => new { e.Level, e.TowerId }).HasName("tower_level_pkey");

            entity.ToTable("tower_level");

            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.TowerId).HasColumnName("tower_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Damage)
                .HasPrecision(10, 2)
                .HasColumnName("damage");
            entity.Property(e => e.FireRate)
                .HasPrecision(10, 2)
                .HasColumnName("fire_rate");
            entity.Property(e => e.Range)
                .HasPrecision(10, 2)
                .HasColumnName("range");
        });

        modelBuilder.Entity<TowerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tower_type_pkey");

            entity.ToTable("tower_type");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuidv7()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Wave>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("wave_pkey");

            entity.ToTable("wave");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<WaveSpawnGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("wave_spawn_group");

            entity.HasIndex(e => new { e.Index, e.WaveId }, "wave_spawn_group_index_wave_id_key").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.WaveId).HasColumnName("wave_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
