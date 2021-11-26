INSERT INTO "Genres" ("Name")
VALUES ('RPG');
INSERT INTO "Genres" ("Name")
VALUES ('Action Real Time Strategy');
INSERT INTO "Genres" ("Name")
VALUES ('Racing');
INSERT INTO "Genres" ("Name")
VALUES ('Platform');
INSERT INTO "Genres" ("Name")
VALUES ('Strategy');
INSERT INTO "Genres" ("Name")
VALUES ('Action');

INSERT INTO "Platforms" ("Name")
VALUES ('Windows');
INSERT INTO "Platforms" ("Name")
VALUES ('Linux');
INSERT INTO "Platforms" ("Name")
VALUES ('Mac');

INSERT INTO "Players" ("Name")
VALUES ('Одиночная игра');
INSERT INTO "Players" ("Name")
VALUES ('Кооперативная игра');
INSERT INTO "Players" ("Name")
VALUES ('Многопользовательская игра');

INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (1, 'Dota 2', '2011-10-21',
        'Компьютерная многопользовательская командная игра в жанре multiplayer online battle arena, разработанная корпорацией Valve. Игра является продолжением DotA - пользовательской карты-модификации для игры Warcraft III: Reign of Chaos и дополнения к ней Warcraft III: The Frozen Throne. Игра изображает сражение на карте особого вида; в каждом матче участвуют две команды по пять игроков, управляющих «героями» - персонажами с различными наборами способностей. Для победы в матче команда должна уничтожить особый объект-«крепость», принадлежащий вражеской стороне, и защитить от уничтожения собственную «крепость». Dota 2 работает по модели free-to-play с элементами микроплатежей.',
        'https://yt3.ggpht.com/a/AATXAJw2sD6P_xdkOHetBMTO62dHc-HYeziYUvsenyeZ=s900-c-k-c0xffffffff-no-rj-mo', 3000);
INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (2, 'GTA 4', '2009-09-12',
        'Мультиплатформенная трёхмерная компьютерная игра в жанре action-adventure, девятая в серии Grand Theft Auto, выпущена 29 апреля 2008 года для двух игровых приставок - PlayStation 3 и Xbox 360, также полгода спустя игру портировали на ПК. Компания Rockstar выпустила дополнения к игре, распространяя их через интернет-сервисы Xbox Live, Games for Windows - Live, PlayStation Network и в составе дискового издания Grand Theft Auto: Episodes from Liberty City; в этих дополнениях под управлением игрока находятся новые герои, и сюжеты дополнений происходят параллельно сюжету основной игры. Действие Grand Theft Auto IV происходит в вымышленном американском городе Либерти-Сити, прообразом которого послужил Нью-Йорк',
        'https://www.digiseller.ru/preview/828276/p1_2894461_1c8d45ca.jpg', 5000);
INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (3, 'GTA 5', '2013-09-13',
        'Мультиплатформенная компьютерная игра в жанре action-adventure с открытым миром, разработанная компанией Rockstar North и изданная компанией Rockstar Games. Изначально игра была выпущена для игровых консолей PlayStation 3 и Xbox 360 в 2013 году, в 2014 году переиздана для PlayStation 4 и Xbox One, а в 2015 году - для персональных компьютеров под управлением Windows. Является пятнадцатой по счёту игрой серии Grand Theft Auto и следующей крупной игрой после Grand Theft Auto IV, выпущенной в 2008 году. В России и СНГ издателем Grand Theft Auto V выступает компания 1С-СофтКлаб.',
        'https://c.allegroimg.com/original/01f58d/d4e8efa04de1b85504ba6de2091c/GRAND-THEFT-AUTO-GTA-V-5-KLUCZ-CYFROWY-ROCKSTAR',
        2000);
INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (4, 'GTA 3', '2001-05-02',
        'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a,',
        'https://static.daru-dar.org/s1024/01/00/81/3e/813e7ecec4d548e16f17d97dfe951e8cc096d8b1.jpg', 1500);
INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (5, 'CS:GO', '2012-05-02',
        'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a,',
        'https://esports-24.ru/wp-content/uploads/2021/01/gfrewr342432.jpg', 1299);
INSERT INTO "Games" ("Id", "Title", "ReleaseDate", "Description", "Cover", "Price")
VALUES (6, 'Warface', '2013-05-02',
        'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a,',
        'https://game-tips.ru/wp-content/uploads/2017/06/Warface.jpg', 6999);

INSERT INTO "GameGenres"
VALUES (1, 1);
INSERT INTO "GameGenres"
VALUES (2, 2);
INSERT INTO "GameGenres"
VALUES (3, 3);
INSERT INTO "GameGenres"
VALUES (4, 4);
INSERT INTO "GameGenres"
VALUES (5, 5);
INSERT INTO "GameGenres"
VALUES (6, 6);

INSERT INTO "GamePlatforms"
VALUES (1, 1);
INSERT INTO "GamePlatforms"
VALUES (2, 2);
INSERT INTO "GamePlatforms"
VALUES (3, 3);
INSERT INTO "GamePlatforms"
VALUES (4, 1);
INSERT INTO "GamePlatforms"
VALUES (5, 2);
INSERT INTO "GamePlatforms"
VALUES (6, 3);

INSERT INTO "GamePlayers"
VALUES (1, 1);
INSERT INTO "GamePlayers"
VALUES (2, 2);
INSERT INTO "GamePlayers"
VALUES (3, 3);
INSERT INTO "GamePlayers"
VALUES (4, 1);
INSERT INTO "GamePlayers"
VALUES (5, 2);
INSERT INTO "GamePlayers"
VALUES (6, 3);