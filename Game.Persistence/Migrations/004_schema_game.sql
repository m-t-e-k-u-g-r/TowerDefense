CREATE TABLE game_configuration (
    id UUID PRIMARY KEY DEFAULT uuidv7(),
    field_id INT NOT NULL,
    FOREIGN KEY (field_id) REFERENCES field(id) ON DELETE CASCADE
);

CREATE TABLE game_wave (
    game_id UUID NOT NULL,
    wave_id INT NOT NULL UNIQUE,
    index INT NOT NULL,
    PRIMARY KEY (game_id, wave_id),
    UNIQUE (game_id, index),
    FOREIGN KEY (game_id) REFERENCES game_configuration(id) ON DELETE CASCADE,
    FOREIGN KEY (wave_id) REFERENCES wave(id) ON DELETE CASCADE
);
