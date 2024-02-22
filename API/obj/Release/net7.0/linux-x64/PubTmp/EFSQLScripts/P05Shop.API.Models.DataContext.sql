IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028221446_Initialization')
BEGIN
    CREATE TABLE [Books] (
        [Id] decimal(8,0) NOT NULL,
        [Title] nvarchar(100) NOT NULL,
        [Author] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028221446_Initialization')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Author', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
        SET IDENTITY_INSERT [Books] ON;
    EXEC(N'INSERT INTO [Books] ([Id], [Author], [Title])
    VALUES (1.0, N''Sydney Roberts'', N''maiores''),
    (2.0, N''Myrtice Beahan'', N''vel''),
    (3.0, N''Camden Dietrich'', N''sit''),
    (4.0, N''Tabitha Kunze'', N''et''),
    (5.0, N''Jailyn Block'', N''consectetur''),
    (6.0, N''Kennith Gaylord'', N''nulla''),
    (7.0, N''Tristin Cronin'', N''consequatur''),
    (8.0, N''Orval Abernathy'', N''accusantium''),
    (9.0, N''Louvenia Pfannerstill'', N''facere''),
    (10.0, N''Jovan Connelly'', N''eveniet''),
    (11.0, N''Royal Rosenbaum'', N''mollitia'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Author', N'Title') AND [object_id] = OBJECT_ID(N'[Books]'))
        SET IDENTITY_INSERT [Books] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231028221446_Initialization')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231028221446_Initialization', N'7.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231112101153_letthiswork')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231112101153_letthiswork', N'7.0.13');
END;
GO

COMMIT;
GO

