INSERT INTO roles (name) VALUES ('customer');
INSERT INTO roles (name) VALUES ('developer');
INSERT INTO roles (name) VALUES ('admin');

INSERT INTO genres (name) VALUES ('RPG');
INSERT INTO genres (name) VALUES ('Action Real Time Strategy');
INSERT INTO genres (name) VALUES ('Racing');
INSERT INTO genres (name) VALUES ('Platform');

INSERT INTO users (nickname) VALUES ('taiiiga');

INSERT INTO games (id, title, release_date, description, cover) 
VALUES (1, 'Dota 2', '2011-10-21', 'Компьютерная многопользовательская командная игра в жанре multiplayer online battle arena, разработанная корпорацией Valve. Игра является продолжением DotA - пользовательской карты-модификации для игры Warcraft III: Reign of Chaos и дополнения к ней Warcraft III: The Frozen Throne. Игра изображает сражение на карте особого вида; в каждом матче участвуют две команды по пять игроков, управляющих «героями» - персонажами с различными наборами способностей. Для победы в матче команда должна уничтожить особый объект-«крепость», принадлежащий вражеской стороне, и защитить от уничтожения собственную «крепость». Dota 2 работает по модели free-to-play с элементами микроплатежей.', 'https://yt3.ggpht.com/a/AATXAJw2sD6P_xdkOHetBMTO62dHc-HYeziYUvsenyeZ=s900-c-k-c0xffffffff-no-rj-mo');
INSERT INTO games (id, title, release_date, description, cover) 
VALUES (2, 'GTA 4', '2009-09-12', 'Мультиплатформенная трёхмерная компьютерная игра в жанре action-adventure, девятая в серии Grand Theft Auto, выпущена 29 апреля 2008 года для двух игровых приставок - PlayStation 3 и Xbox 360, также полгода спустя игру портировали на ПК. Компания Rockstar выпустила дополнения к игре, распространяя их через интернет-сервисы Xbox Live, Games for Windows - Live, PlayStation Network и в составе дискового издания Grand Theft Auto: Episodes from Liberty City; в этих дополнениях под управлением игрока находятся новые герои, и сюжеты дополнений происходят параллельно сюжету основной игры. Действие Grand Theft Auto IV происходит в вымышленном американском городе Либерти-Сити, прообразом которого послужил Нью-Йорк', 'https://www.digiseller.ru/preview/828276/p1_2894461_1c8d45ca.jpg');
INSERT INTO games (id, title, release_date, description, cover) 
VALUES (3, 'GTA 5', '2013-09-13', 'Мультиплатформенная компьютерная игра в жанре action-adventure с открытым миром, разработанная компанией Rockstar North и изданная компанией Rockstar Games. Изначально игра была выпущена для игровых консолей PlayStation 3 и Xbox 360 в 2013 году, в 2014 году переиздана для PlayStation 4 и Xbox One, а в 2015 году - для персональных компьютеров под управлением Windows. Является пятнадцатой по счёту игрой серии Grand Theft Auto и следующей крупной игрой после Grand Theft Auto IV, выпущенной в 2008 году. В России и СНГ издателем Grand Theft Auto V выступает компания 1С-СофтКлаб.', 'https://c.allegroimg.com/original/01f58d/d4e8efa04de1b85504ba6de2091c/GRAND-THEFT-AUTO-GTA-V-5-KLUCZ-CYFROWY-ROCKSTAR');

INSERT INTO games_genres VALUES (1, 2);
INSERT INTO users_roles VALUES (1, 3);
INSERT INTO users_games VALUES (1, 1);
