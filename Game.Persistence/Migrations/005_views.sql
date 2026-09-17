CREATE OR REPLACE VIEW computed_tower AS
SELECT
    t.*,
    jsonb_agg(
        jsonb_build_object(
            'level', tl.level,
            'cost', tl.cost,
            'damage', tl.damage,
            'range', tl.range,
            'fire_rate', tl.fire_rate
        )
    ) AS levels
FROM tower_type t
    JOIN tower_level tl ON t.id = tl.tower_id
GROUP BY t.id;

CREATE OR REPLACE VIEW computed_wave AS
SELECT
    w.id,
    gw.game_id,
    gw.index,
    w.name,
    w.duration,
    jsonb_agg(to_jsonb(sg) - 'id') AS spawn_groups
FROM wave w
    JOIN wave_spawn_group wsg ON w.id = wsg.wave_id
    JOIN spawn_group sg ON wsg.group_id = sg.id
    JOIN game_wave gw ON gw.wave_id = w.id
GROUP BY
    w.id,
    gw.game_id,
    gw.index;

CREATE OR REPLACE VIEW game_config AS
SELECT
    gc.id,
    f.width,
    f.height,
    (
        SELECT jsonb_agg(
            jsonb_build_object(
                'id', p.id,
                'color', p.color,
                'tiles', p.enemy_path
            )
       )
        FROM path p
        WHERE p.field_id = f.id
    ) AS paths
FROM game_configuration gc
    JOIN field f ON gc.field_id = f.id;
