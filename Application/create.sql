DROP TABLE games_genres;
DROP TABLE users_roles;
DROP TABLE users_games;
DROP TABLE users;
DROP TABLE roles;
DROP TABLE genres;
DROP TABLE games;

CREATE TABLE IF NOT EXISTS users
(
    id   BIGSERIAL PRIMARY KEY,
    nickname VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS roles
(
    id   BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS genres
(
    id   BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE games
(
    id           BIGSERIAL PRIMARY KEY,
    title        VARCHAR(255) NOT NULL UNIQUE,
    release_date DATE NOT NULL,
    description  TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS games_genres
(
    game_id  BIGINT REFERENCES games (id),
    genre_id BIGINT REFERENCES genres (id),
    PRIMARY KEY(game_id, genre_id)
);

CREATE TABLE IF NOT EXISTS users_roles
(
    user_id  BIGINT REFERENCES users (id),
    role_id BIGINT REFERENCES roles (id),
    PRIMARY KEY(user_id, role_id)
);

CREATE TABLE IF NOT EXISTS users_games
(
    user_id  BIGINT REFERENCES users (id),
    game_id  BIGINT REFERENCES games (id),
    PRIMARY KEY(user_id, game_id)
);