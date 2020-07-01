IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Activity] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] VARCHAR(24) NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] VARCHAR(24) NULL,
    [Title] VARCHAR(50) NULL,
    [Description] VARCHAR(150) NULL,
    [Category] VARCHAR(50) NULL,
    [Date] datetime2 NOT NULL,
    [City] VARCHAR(50) NULL,
    [Venue] VARCHAR(50) NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AppUser] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] VARCHAR(24) NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] VARCHAR(24) NULL,
    [FirstName] VARCHAR(24) NULL,
    [LastName] VARCHAR(24) NULL,
    [Email] VARCHAR(24) NULL,
    [Bio] VARCHAR(240) NULL,
    CONSTRAINT [PK_AppUser] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Value] (
    [Id] int NOT NULL IDENTITY,
    [Name] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Value] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [IdentityUser] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] VARCHAR(24) NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] VARCHAR(24) NULL,
    [UserName] VARCHAR(24) NULL,
    [Passoword] VARCHAR(50) NULL,
    [Salt] VARCHAR(50) NULL,
    [AppUserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_IdentityUser] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityUser_AppUser_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUser] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Photo] (
    [Id] uniqueidentifier NOT NULL,
    [IsMainPhoto] bit NOT NULL,
    [ActualFileName] VARCHAR(50) NULL,
    [CloudFileName] VARCHAR(50) NULL,
    [ContentType] VARCHAR(50) NULL,
    [Length] bigint NOT NULL,
    [UploadedDate] datetime2 NOT NULL,
    [AppUserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Photo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Photo_AppUser_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUser] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [UserActivity] (
    [AppUserId] uniqueidentifier NOT NULL,
    [ActivityId] uniqueidentifier NOT NULL,
    [DateJoined] datetime2 NOT NULL,
    [IsHost] bit NOT NULL,
    CONSTRAINT [PK_UserActivity] PRIMARY KEY ([ActivityId], [AppUserId]),
    CONSTRAINT [FK_UserActivity_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activity] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserActivity_AppUser_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AppUser] ([Id]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] ON;
INSERT INTO [Activity] ([Id], [Category], [City], [CreatedBy], [CreatedDate], [Date], [Description], [Title], [UpdatedBy], [UpdatedDate], [Venue])
VALUES ('eb1f7fd9-a352-4907-862a-8b0f7e975853', 'drinks', 'London', 'Seed', '2020-06-21T19:07:41.6168543-04:00', '2020-04-21T19:07:41.6095494-04:00', 'Activity 2 months ago', 'Past Activity 1', 'Seed', '2020-06-21T19:07:41.6169734-04:00', 'Pub'),
('8f967057-de73-49ac-89e6-8e2b5c64b840', 'film', 'London', 'Seed', '2020-06-21T19:07:41.6171761-04:00', '2021-02-21T19:07:41.6160747-05:00', 'Activity 8 months in future', 'Future Activity 8', 'Seed', '2020-06-21T19:07:41.6171768-04:00', 'Cinema'),
('3f3434ba-268a-4a67-987e-52a76f96c130', 'music', 'London', 'Seed', '2020-06-21T19:07:41.6171729-04:00', '2020-12-21T19:07:41.6160717-05:00', 'Activity 6 months in future', 'Future Activity 6', 'Seed', '2020-06-21T19:07:41.6171736-04:00', 'Roundhouse Camden'),
('3c738613-6312-4d62-bf14-93def26593e6', 'drinks', 'London', 'Seed', '2020-06-21T19:07:41.6171701-04:00', '2020-11-21T19:07:41.6160704-05:00', 'Activity 5 months in future', 'Future Activity 5', 'Seed', '2020-06-21T19:07:41.6171708-04:00', 'Just another pub'),
('39427a54-8315-447b-9bb2-990db9ce6afa', 'drinks', 'London', 'Seed', '2020-06-21T19:07:41.6171685-04:00', '2020-10-21T19:07:41.6160692-04:00', 'Activity 4 months in future', 'Future Activity 4', 'Seed', '2020-06-21T19:07:41.6171692-04:00', 'Yet another pub'),
('13ed81c7-6b31-411f-b945-e529d0fc7c69', 'travel', 'London', 'Seed', '2020-06-21T19:07:41.6171745-04:00', '2021-01-21T19:07:41.6160730-05:00', 'Activity 2 months ago', 'Future Activity 7', 'Seed', '2020-06-21T19:07:41.6171752-04:00', 'Somewhere on the Thames'),
('d5b6d235-96b8-4020-9b33-5802d245df15', 'music', 'London', 'Seed', '2020-06-21T19:07:41.6171639-04:00', '2020-08-21T19:07:41.6160656-04:00', 'Activity 2 months in future', 'Future Activity 2', 'Seed', '2020-06-21T19:07:41.6171651-04:00', 'O2 Arena'),
('6fb3789b-5a76-4812-9717-5c28b5b9a33c', 'culture', 'London', 'Seed', '2020-06-21T19:07:41.6171611-04:00', '2020-07-21T19:07:41.6160632-04:00', 'Activity 1 month in future', 'Future Activity 1', 'Seed', '2020-06-21T19:07:41.6171624-04:00', 'Natural History Museum'),
('efed3e18-0910-4bd8-aa70-17fce47c3cba', 'culture', 'Paris', 'Seed', '2020-06-21T19:07:41.6171417-04:00', '2020-05-21T19:07:41.6160377-04:00', 'Activity 1 month ago', 'Past Activity 2', 'Seed', '2020-06-21T19:07:41.6171533-04:00', 'Louvre'),
('f4676b1a-d1c3-43c3-b737-781370d66144', 'drinks', 'London', 'Seed', '2020-06-21T19:07:41.6171666-04:00', '2020-09-21T19:07:41.6160670-04:00', 'Activity 3 months in future', 'Future Activity 3', 'Seed', '2020-06-21T19:07:41.6171674-04:00', 'Another pub');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] ON;
INSERT INTO [AppUser] ([Id], [Bio], [CreatedBy], [CreatedDate], [Email], [FirstName], [LastName], [UpdatedBy], [UpdatedDate])
VALUES ('b0c2f35c-77ba-4b69-9c74-97d02f3aa27c', NULL, 'Seed', '2020-06-21T19:07:41.6197630-04:00', 'JohnDoe@domain.com', 'John', 'Doe', 'Seed', '2020-06-21T19:07:41.6197837-04:00'),
('78151b38-ad7f-4e27-b30d-3d8690c73237', NULL, 'Seed', '2020-06-21T19:07:41.6197949-04:00', 'Jane.Smith@domain.com', 'Jane', 'Smith', 'Seed', '2020-06-21T19:07:41.6197962-04:00'),
('551e9172-9202-4985-9865-4df2f53fcf2b', NULL, 'Seed', '2020-06-21T19:07:41.6197982-04:00', 'Bruce.Lee@domain.com', 'Bruce', 'Lee', 'Seed', '2020-06-21T19:07:41.6197990-04:00'),
('b4872b74-272e-42f2-9631-e43baec8a059', NULL, 'Seed', '2020-06-21T19:07:41.6198003-04:00', 'NP@domain.com', 'Nij', 'Patel', 'Seed', '2020-06-21T19:07:41.6198015-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Value]'))
    SET IDENTITY_INSERT [Value] ON;
INSERT INTO [Value] ([Id], [Name])
VALUES (2, 'Value 201'),
(1, 'Value 101'),
(3, 'Value 301');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Value]'))
    SET IDENTITY_INSERT [Value] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] ON;
INSERT INTO [IdentityUser] ([Id], [AppUserId], [CreatedBy], [CreatedDate], [Passoword], [Salt], [UpdatedBy], [UpdatedDate], [UserName])
VALUES ('aaf7dec0-ce9b-4e49-afe4-498376e0e307', 'b0c2f35c-77ba-4b69-9c74-97d02f3aa27c', 'Seed', '2020-06-21T19:07:41.6220586-04:00', '/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=', 'St0OnTE2Ju3Li9uSnlz/Mg==', 'Seed', '2020-06-21T19:07:41.6220873-04:00', 'JohnDoe@domain.com'),
('b1115bf2-5639-4f48-a710-f02933c82c4d', '78151b38-ad7f-4e27-b30d-3d8690c73237', 'Seed', '2020-06-21T19:07:41.6221585-04:00', 'S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=', 'f9/SzZwluz+xI51/VQQIzg==', 'Seed', '2020-06-21T19:07:41.6221598-04:00', 'Jane.Smith@domain.com'),
('de0ce645-0350-43a4-9ef3-7676dac4824e', '551e9172-9202-4985-9865-4df2f53fcf2b', 'Seed', '2020-06-21T19:07:41.6221613-04:00', '5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=', 'DEX8D+3HR9flD6NpGibucQ==', 'Seed', '2020-06-21T19:07:41.6221620-04:00', 'Bruce.Lee@domain.com'),
('983d5598-0960-43a0-9f7d-a9c3f4e2e751', 'b4872b74-272e-42f2-9631-e43baec8a059', 'Seed', '2020-06-21T19:07:41.6221631-04:00', 'k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=', 'tycaGrI7zbrlLUa1rlq/Eg==', 'Seed', '2020-06-21T19:07:41.6221638-04:00', 'string');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] OFF;

GO

CREATE UNIQUE INDEX [IX_IdentityUser_AppUserId] ON [IdentityUser] ([AppUserId]);

GO

CREATE INDEX [IX_Photo_AppUserId] ON [Photo] ([AppUserId]);

GO

CREATE INDEX [IX_UserActivity_AppUserId] ON [UserActivity] ([AppUserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200621230742_InitialMigration', N'3.1.5');

GO

DELETE FROM [Activity]
WHERE [Id] = '13ed81c7-6b31-411f-b945-e529d0fc7c69';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '39427a54-8315-447b-9bb2-990db9ce6afa';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '3c738613-6312-4d62-bf14-93def26593e6';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '3f3434ba-268a-4a67-987e-52a76f96c130';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '6fb3789b-5a76-4812-9717-5c28b5b9a33c';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '8f967057-de73-49ac-89e6-8e2b5c64b840';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'd5b6d235-96b8-4020-9b33-5802d245df15';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'eb1f7fd9-a352-4907-862a-8b0f7e975853';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'efed3e18-0910-4bd8-aa70-17fce47c3cba';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'f4676b1a-d1c3-43c3-b737-781370d66144';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = '983d5598-0960-43a0-9f7d-a9c3f4e2e751';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = 'aaf7dec0-ce9b-4e49-afe4-498376e0e307';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = 'b1115bf2-5639-4f48-a710-f02933c82c4d';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = 'de0ce645-0350-43a4-9ef3-7676dac4824e';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = '551e9172-9202-4985-9865-4df2f53fcf2b';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = '78151b38-ad7f-4e27-b30d-3d8690c73237';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = 'b0c2f35c-77ba-4b69-9c74-97d02f3aa27c';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = 'b4872b74-272e-42f2-9631-e43baec8a059';
SELECT @@ROWCOUNT;


GO

CREATE TABLE [Comment] (
    [Id] uniqueidentifier NOT NULL,
    [Body] VARCHAR(240) NULL,
    [AuthorId] uniqueidentifier NOT NULL,
    [ActivityId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comment_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activity] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comment_AppUser_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AppUser] ([Id]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] ON;
INSERT INTO [Activity] ([Id], [Category], [City], [CreatedBy], [CreatedDate], [Date], [Description], [Title], [UpdatedBy], [UpdatedDate], [Venue])
VALUES ('2b7fd22d-f0ee-4a50-a6fb-871b8fd06a1d', 'drinks', 'London', 'Seed', '2020-06-29T18:24:09.3655021-04:00', '2020-04-29T18:24:09.3590034-04:00', 'Activity 2 months ago', 'Past Activity 1', 'Seed', '2020-06-29T18:24:09.3656317-04:00', 'Pub'),
('9192118e-bc9a-4775-bf25-922f4e81629c', 'culture', 'Paris', 'Seed', '2020-06-29T18:24:09.3657550-04:00', '2020-05-29T18:24:09.3648609-04:00', 'Activity 1 month ago', 'Past Activity 2', 'Seed', '2020-06-29T18:24:09.3657628-04:00', 'Louvre'),
('1b25ee77-4ea9-4812-902f-cdfb538c874f', 'culture', 'London', 'Seed', '2020-06-29T18:24:09.3657673-04:00', '2020-07-29T18:24:09.3648821-04:00', 'Activity 1 month in future', 'Future Activity 1', 'Seed', '2020-06-29T18:24:09.3657680-04:00', 'Natural History Museum'),
('fef6aef4-be94-4b36-b814-5d825bbe6f2a', 'music', 'London', 'Seed', '2020-06-29T18:24:09.3657710-04:00', '2020-08-29T18:24:09.3648840-04:00', 'Activity 2 months in future', 'Future Activity 2', 'Seed', '2020-06-29T18:24:09.3657717-04:00', 'O2 Arena'),
('569f94f0-3e42-4160-96ab-bd7a79920f1c', 'drinks', 'London', 'Seed', '2020-06-29T18:24:09.3657726-04:00', '2020-09-29T18:24:09.3648850-04:00', 'Activity 3 months in future', 'Future Activity 3', 'Seed', '2020-06-29T18:24:09.3657733-04:00', 'Another pub'),
('29fcdf4a-6edf-43e8-b23e-234b7ebdf2e1', 'drinks', 'London', 'Seed', '2020-06-29T18:24:09.3657741-04:00', '2020-10-29T18:24:09.3648867-04:00', 'Activity 4 months in future', 'Future Activity 4', 'Seed', '2020-06-29T18:24:09.3657747-04:00', 'Yet another pub'),
('e9c12889-6947-4224-a8ce-7212f929c7f2', 'drinks', 'London', 'Seed', '2020-06-29T18:24:09.3657755-04:00', '2020-11-29T18:24:09.3648876-05:00', 'Activity 5 months in future', 'Future Activity 5', 'Seed', '2020-06-29T18:24:09.3657762-04:00', 'Just another pub'),
('224c2d36-98dc-4e69-9e07-1e87f4219e74', 'music', 'London', 'Seed', '2020-06-29T18:24:09.3657771-04:00', '2020-12-29T18:24:09.3648885-05:00', 'Activity 6 months in future', 'Future Activity 6', 'Seed', '2020-06-29T18:24:09.3657777-04:00', 'Roundhouse Camden'),
('7138ce38-45c6-4412-b816-7879c4c332d0', 'travel', 'London', 'Seed', '2020-06-29T18:24:09.3657785-04:00', '2021-01-29T18:24:09.3648894-05:00', 'Activity 2 months ago', 'Future Activity 7', 'Seed', '2020-06-29T18:24:09.3657792-04:00', 'Somewhere on the Thames'),
('ce788c7f-df8b-416f-bdf7-ccc0cb66b0ee', 'film', 'London', 'Seed', '2020-06-29T18:24:09.3657801-04:00', '2021-02-28T18:24:09.3648908-05:00', 'Activity 8 months in future', 'Future Activity 8', 'Seed', '2020-06-29T18:24:09.3657807-04:00', 'Cinema');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] ON;
INSERT INTO [AppUser] ([Id], [Bio], [CreatedBy], [CreatedDate], [Email], [FirstName], [LastName], [UpdatedBy], [UpdatedDate])
VALUES ('f4dab947-cb80-403c-ac06-0b4d8f43d86f', NULL, 'Seed', '2020-06-29T18:24:09.3675707-04:00', 'JohnDoe@domain.com', 'John', 'Doe', 'Seed', '2020-06-29T18:24:09.3675831-04:00'),
('bf0f0369-0e79-443c-a4be-2e096cfadc19', NULL, 'Seed', '2020-06-29T18:24:09.3675934-04:00', 'Jane.Smith@domain.com', 'Jane', 'Smith', 'Seed', '2020-06-29T18:24:09.3675944-04:00'),
('75da9d7c-fea3-4f22-862d-e5a40d004e65', NULL, 'Seed', '2020-06-29T18:24:09.3675955-04:00', 'Bruce.Lee@domain.com', 'Bruce', 'Lee', 'Seed', '2020-06-29T18:24:09.3675961-04:00'),
('41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905', NULL, 'Seed', '2020-06-29T18:24:09.3675971-04:00', 'NP@domain.com', 'Nij', 'Patel', 'Seed', '2020-06-29T18:24:09.3675977-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] ON;
INSERT INTO [IdentityUser] ([Id], [AppUserId], [CreatedBy], [CreatedDate], [Passoword], [Salt], [UpdatedBy], [UpdatedDate], [UserName])
VALUES ('864c11fe-e06b-4f54-a504-f94376c8f770', 'f4dab947-cb80-403c-ac06-0b4d8f43d86f', 'Seed', '2020-06-29T18:24:09.3693893-04:00', '/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=', 'St0OnTE2Ju3Li9uSnlz/Mg==', 'Seed', '2020-06-29T18:24:09.3693987-04:00', 'JohnDoe@domain.com'),
('98221c13-b39e-4058-a52f-b87c6ff95c98', 'bf0f0369-0e79-443c-a4be-2e096cfadc19', 'Seed', '2020-06-29T18:24:09.3694383-04:00', 'S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=', 'f9/SzZwluz+xI51/VQQIzg==', 'Seed', '2020-06-29T18:24:09.3694396-04:00', 'Jane.Smith@domain.com'),
('c7112578-f044-4f48-9f41-7a8842f0d1ef', '75da9d7c-fea3-4f22-862d-e5a40d004e65', 'Seed', '2020-06-29T18:24:09.3694412-04:00', '5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=', 'DEX8D+3HR9flD6NpGibucQ==', 'Seed', '2020-06-29T18:24:09.3694418-04:00', 'Bruce.Lee@domain.com'),
('693017b7-fd2d-431f-a5ca-6bf903101e48', '41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905', 'Seed', '2020-06-29T18:24:09.3694430-04:00', 'k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=', 'tycaGrI7zbrlLUa1rlq/Eg==', 'Seed', '2020-06-29T18:24:09.3694437-04:00', 'string');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] OFF;

GO

CREATE INDEX [IX_Comment_ActivityId] ON [Comment] ([ActivityId]);

GO

CREATE INDEX [IX_Comment_AuthorId] ON [Comment] ([AuthorId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200629222410_Comment', N'3.1.5');

GO

DELETE FROM [Activity]
WHERE [Id] = '1b25ee77-4ea9-4812-902f-cdfb538c874f';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '224c2d36-98dc-4e69-9e07-1e87f4219e74';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '29fcdf4a-6edf-43e8-b23e-234b7ebdf2e1';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '2b7fd22d-f0ee-4a50-a6fb-871b8fd06a1d';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '569f94f0-3e42-4160-96ab-bd7a79920f1c';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '7138ce38-45c6-4412-b816-7879c4c332d0';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = '9192118e-bc9a-4775-bf25-922f4e81629c';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'ce788c7f-df8b-416f-bdf7-ccc0cb66b0ee';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'e9c12889-6947-4224-a8ce-7212f929c7f2';
SELECT @@ROWCOUNT;


GO

DELETE FROM [Activity]
WHERE [Id] = 'fef6aef4-be94-4b36-b814-5d825bbe6f2a';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = '693017b7-fd2d-431f-a5ca-6bf903101e48';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = '864c11fe-e06b-4f54-a504-f94376c8f770';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = '98221c13-b39e-4058-a52f-b87c6ff95c98';
SELECT @@ROWCOUNT;


GO

DELETE FROM [IdentityUser]
WHERE [Id] = 'c7112578-f044-4f48-9f41-7a8842f0d1ef';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = '41ecd0fd-8ff4-4c1d-aed6-8158ad8e1905';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = '75da9d7c-fea3-4f22-862d-e5a40d004e65';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = 'bf0f0369-0e79-443c-a4be-2e096cfadc19';
SELECT @@ROWCOUNT;


GO

DELETE FROM [AppUser]
WHERE [Id] = 'f4dab947-cb80-403c-ac06-0b4d8f43d86f';
SELECT @@ROWCOUNT;


GO

CREATE TABLE [UserFollower] (
    [UserId] uniqueidentifier NOT NULL,
    [FollowerId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserFollower] PRIMARY KEY ([UserId], [FollowerId]),
    CONSTRAINT [FK_UserFollower_AppUser_FollowerId] FOREIGN KEY ([FollowerId]) REFERENCES [AppUser] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserFollower_AppUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] ON;
INSERT INTO [Activity] ([Id], [Category], [City], [CreatedBy], [CreatedDate], [Date], [Description], [Title], [UpdatedBy], [UpdatedDate], [Venue])
VALUES ('fcda0ee6-0b33-4402-a42e-ebdeaba3c027', 'drinks', 'London', 'Seed', '2020-06-30T23:52:42.8025681-04:00', '2020-04-30T23:52:42.7970897-04:00', 'Activity 2 months ago', 'Past Activity 1', 'Seed', '2020-06-30T23:52:42.8026385-04:00', 'Pub'),
('43a35040-876a-4694-b49a-c0430749d1fd', 'culture', 'Paris', 'Seed', '2020-06-30T23:52:42.8027143-04:00', '2020-05-30T23:52:42.8021056-04:00', 'Activity 1 month ago', 'Past Activity 2', 'Seed', '2020-06-30T23:52:42.8027268-04:00', 'Louvre'),
('383b73d9-581d-4825-bcff-5960142c1528', 'culture', 'London', 'Seed', '2020-06-30T23:52:42.8027489-04:00', '2020-07-30T23:52:42.8021208-04:00', 'Activity 1 month in future', 'Future Activity 1', 'Seed', '2020-06-30T23:52:42.8027509-04:00', 'Natural History Museum'),
('e61a5a31-c4b6-4d9b-a479-4522a117708d', 'music', 'London', 'Seed', '2020-06-30T23:52:42.8027515-04:00', '2020-08-30T23:52:42.8021220-04:00', 'Activity 2 months in future', 'Future Activity 2', 'Seed', '2020-06-30T23:52:42.8027519-04:00', 'O2 Arena'),
('58768f77-8fcd-42f6-b9d8-de4f1d4cf8df', 'drinks', 'London', 'Seed', '2020-06-30T23:52:42.8027525-04:00', '2020-09-30T23:52:42.8021225-04:00', 'Activity 3 months in future', 'Future Activity 3', 'Seed', '2020-06-30T23:52:42.8027530-04:00', 'Another pub'),
('ae685e8e-2521-4469-9ba2-6ecb935f6ec6', 'drinks', 'London', 'Seed', '2020-06-30T23:52:42.8027535-04:00', '2020-10-30T23:52:42.8021240-04:00', 'Activity 4 months in future', 'Future Activity 4', 'Seed', '2020-06-30T23:52:42.8027540-04:00', 'Yet another pub'),
('b81b1e3d-7c2b-4b41-95de-1ac2129b4bd4', 'drinks', 'London', 'Seed', '2020-06-30T23:52:42.8027559-04:00', '2020-11-30T23:52:42.8021245-05:00', 'Activity 5 months in future', 'Future Activity 5', 'Seed', '2020-06-30T23:52:42.8027563-04:00', 'Just another pub'),
('5753186c-dccc-4196-b75f-cfdab0aa6cd6', 'music', 'London', 'Seed', '2020-06-30T23:52:42.8027569-04:00', '2020-12-30T23:52:42.8021251-05:00', 'Activity 6 months in future', 'Future Activity 6', 'Seed', '2020-06-30T23:52:42.8027573-04:00', 'Roundhouse Camden'),
('6f81c396-3161-4a9d-acf1-648e71a6a98b', 'travel', 'London', 'Seed', '2020-06-30T23:52:42.8027578-04:00', '2021-01-30T23:52:42.8021257-05:00', 'Activity 2 months ago', 'Future Activity 7', 'Seed', '2020-06-30T23:52:42.8027583-04:00', 'Somewhere on the Thames'),
('5124498c-1451-450d-a4c1-9c80b9f43b4c', 'film', 'London', 'Seed', '2020-06-30T23:52:42.8027588-04:00', '2021-02-28T23:52:42.8021265-05:00', 'Activity 8 months in future', 'Future Activity 8', 'Seed', '2020-06-30T23:52:42.8027592-04:00', 'Cinema');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Category', N'City', N'CreatedBy', N'CreatedDate', N'Date', N'Description', N'Title', N'UpdatedBy', N'UpdatedDate', N'Venue') AND [object_id] = OBJECT_ID(N'[Activity]'))
    SET IDENTITY_INSERT [Activity] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] ON;
INSERT INTO [AppUser] ([Id], [Bio], [CreatedBy], [CreatedDate], [Email], [FirstName], [LastName], [UpdatedBy], [UpdatedDate])
VALUES ('3c5e848f-a6e1-4b5d-9d98-ca792d0bb1bf', NULL, 'Seed', '2020-06-30T23:52:42.8043718-04:00', 'JohnDoe@domain.com', 'John', 'Doe', 'Seed', '2020-06-30T23:52:42.8043833-04:00'),
('430ca1f5-aed7-46a9-923b-39da0ec5b83b', NULL, 'Seed', '2020-06-30T23:52:42.8043907-04:00', 'Jane.Smith@domain.com', 'Jane', 'Smith', 'Seed', '2020-06-30T23:52:42.8043914-04:00'),
('c991a793-4298-4a6a-b45a-8a35955a3af2', NULL, 'Seed', '2020-06-30T23:52:42.8043922-04:00', 'Bruce.Lee@domain.com', 'Bruce', 'Lee', 'Seed', '2020-06-30T23:52:42.8043928-04:00'),
('743fbd05-33eb-431a-9889-6cc1350a3dd7', NULL, 'Seed', '2020-06-30T23:52:42.8043937-04:00', 'NP@domain.com', 'Nij', 'Patel', 'Seed', '2020-06-30T23:52:42.8043943-04:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bio', N'CreatedBy', N'CreatedDate', N'Email', N'FirstName', N'LastName', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[AppUser]'))
    SET IDENTITY_INSERT [AppUser] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] ON;
INSERT INTO [IdentityUser] ([Id], [AppUserId], [CreatedBy], [CreatedDate], [Passoword], [Salt], [UpdatedBy], [UpdatedDate], [UserName])
VALUES ('084f7f36-fa5e-4bf5-a693-ebc375fde0cb', '3c5e848f-a6e1-4b5d-9d98-ca792d0bb1bf', 'Seed', '2020-06-30T23:52:42.8058869-04:00', '/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=', 'St0OnTE2Ju3Li9uSnlz/Mg==', 'Seed', '2020-06-30T23:52:42.8059035-04:00', 'JohnDoe@domain.com'),
('e987411c-5bf5-4103-8900-1fef7fbb080a', '430ca1f5-aed7-46a9-923b-39da0ec5b83b', 'Seed', '2020-06-30T23:52:42.8059484-04:00', 'S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=', 'f9/SzZwluz+xI51/VQQIzg==', 'Seed', '2020-06-30T23:52:42.8059493-04:00', 'Jane.Smith@domain.com'),
('267e9e33-f597-4933-af82-1159ef48b4d5', 'c991a793-4298-4a6a-b45a-8a35955a3af2', 'Seed', '2020-06-30T23:52:42.8059503-04:00', '5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=', 'DEX8D+3HR9flD6NpGibucQ==', 'Seed', '2020-06-30T23:52:42.8059510-04:00', 'Bruce.Lee@domain.com'),
('29fe9311-4c54-456c-bdad-cbea92b63908', '743fbd05-33eb-431a-9889-6cc1350a3dd7', 'Seed', '2020-06-30T23:52:42.8059522-04:00', 'k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=', 'tycaGrI7zbrlLUa1rlq/Eg==', 'Seed', '2020-06-30T23:52:42.8059528-04:00', 'string');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AppUserId', N'CreatedBy', N'CreatedDate', N'Passoword', N'Salt', N'UpdatedBy', N'UpdatedDate', N'UserName') AND [object_id] = OBJECT_ID(N'[IdentityUser]'))
    SET IDENTITY_INSERT [IdentityUser] OFF;

GO

CREATE INDEX [IX_UserFollower_FollowerId] ON [UserFollower] ([FollowerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200701035244_UserFollowerEntity', N'3.1.5');

GO

