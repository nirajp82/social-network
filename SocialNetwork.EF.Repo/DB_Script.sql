TRUNCATE TABLE [dbo].[__EFMigrationsHistory]
GO
DROP TABLE [dbo].[Photo]
GO
GO
DROP TABLE [dbo].[IdentityUser]
GO
DROP TABLE [dbo].[Comment]
GO
DROP TABLE [dbo].[UserFollower]
GO
DROP TABLE [dbo].[UserActivity]
GO
DROP TABLE [dbo].[Activity]
GO
DROP TABLE [dbo].[AppUser]
GO




/****** Object:  Table [dbo].[Activity]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [varchar](24) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](24) NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](150) NULL,
	[Category] [varchar](50) NULL,
	[Date] [datetime2](7) NOT NULL,
	[City] [varchar](50) NULL,
	[Venue] [varchar](50) NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [varchar](24) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](24) NULL,
	[FirstName] [varchar](24) NULL,
	[LastName] [varchar](24) NULL,
	[Email] [varchar](24) NULL,
	[Bio] [varchar](240) NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [uniqueidentifier] NOT NULL,
	[Body] [varchar](240) NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUser](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [varchar](24) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](24) NULL,
	[UserName] [varchar](24) NULL,
	[Passoword] [varchar](50) NULL,
	[Salt] [varchar](50) NULL,
	[RefreshToken] [varchar](50) NULL,
	[RefreshTokenExpiry] [datetime2](7) NULL,
	[AppUserId] [uniqueidentifier] NOT NULL,
	[PreviousRefreshToken] [varchar](50) NULL,
	[PreviousRefreshTokenExpiry] [datetime] NULL,
 CONSTRAINT [PK_IdentityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [uniqueidentifier] NOT NULL,
	[IsMainPhoto] [bit] NOT NULL,
	[ActualFileName] [varchar](50) NULL,
	[CloudFileName] [varchar](50) NULL,
	[ContentType] [varchar](50) NULL,
	[Length] [bigint] NOT NULL,
	[UploadedDate] [datetime2](7) NOT NULL,
	[AppUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivity]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivity](
	[AppUserId] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL,
	[DateJoined] [datetime2](7) NOT NULL,
	[IsHost] [bit] NOT NULL,
 CONSTRAINT [PK_UserActivity] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC,
	[AppUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFollower]    Script Date: 7/15/2020 12:49:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFollower](
	[UserId] [uniqueidentifier] NOT NULL,
	[FollowerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserFollower] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[FollowerId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'00e6b54a-27bf-4e0e-897f-0773c8c2ada5', CAST(N'2020-07-12T20:29:49.0551947' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551952' AS DateTime2), N'Seed', N'Future Activity 6', N'Activity 6 months in future', N'music', CAST(N'2021-01-12T20:29:49.0550346' AS DateTime2), N'London', N'Roundhouse Camden')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'7d9ab25f-2ca9-42fa-b2d9-29d40892a26b', CAST(N'2020-07-12T20:29:49.0551933' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551939' AS DateTime2), N'Seed', N'Future Activity 5', N'Activity 5 months in future', N'drinks', CAST(N'2020-12-12T20:29:49.0550339' AS DateTime2), N'London', N'Just another pub')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'753d5b55-ff88-4a28-a92c-364c3b68736c', CAST(N'2020-07-12T20:29:49.0551919' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551925' AS DateTime2), N'Seed', N'Future Activity 4', N'Activity 4 months in future', N'drinks', CAST(N'2020-11-12T20:29:49.0550330' AS DateTime2), N'London', N'Yet another pub')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'76a7f1e7-f9ee-4df2-9268-3d7bd4a15578', CAST(N'2020-07-12T20:29:49.0551982' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551988' AS DateTime2), N'Seed', N'Future Activity 8', N'Activity 8 months in future', N'film', CAST(N'2021-03-12T20:29:49.0550366' AS DateTime2), N'London', N'Cinema')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'dd0e2d78-9d7e-41f7-a19e-437430941be3', CAST(N'2020-07-12T20:54:09.1944816' AS DateTime2), N'string', CAST(N'2020-07-12T20:54:09.1944745' AS DateTime2), N'string', N'New Activity', N'This is new Activity', N'food', CAST(N'2021-03-01T23:00:00.0000000' AS DateTime2), N'Richmond', N'VA')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'd48ac8d3-7ff4-46c8-b652-5d87afbfd1f7', CAST(N'2020-07-12T20:29:49.0551878' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551885' AS DateTime2), N'Seed', N'Future Activity 1', N'Activity 1 month in future', N'culture', CAST(N'2020-08-12T20:29:49.0550297' AS DateTime2), N'London', N'Natural History Museum')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'f74a0ce5-1bfb-4b94-b0c5-663629ec8588', CAST(N'2020-07-12T20:29:49.0551863' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551870' AS DateTime2), N'Seed', N'Past Activity 2', N'Activity 1 month ago', N'culture', CAST(N'2020-06-12T20:29:49.0550189' AS DateTime2), N'Paris', N'Louvre')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'd3fe056e-2fbd-435b-93f8-79a39142b530', CAST(N'2020-07-12T20:29:49.0551892' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551898' AS DateTime2), N'Seed', N'Future Activity 2', N'Activity 2 months in future', N'music', CAST(N'2020-09-12T20:29:49.0550307' AS DateTime2), N'London', N'O2 Arena')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'e102f5ab-0ded-4a3d-96a0-7bafe78ab476', CAST(N'2020-07-12T20:29:49.0552010' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0552015' AS DateTime2), N'Seed', N'Future Activity 10', N'Activity 10 months in future', N'music', CAST(N'2021-05-12T20:29:49.0550382' AS DateTime2), N'Glen Allen', N'Party Place')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'bcdae06a-2c5e-4019-bdab-b59893df8a08', CAST(N'2020-07-12T20:29:49.0551996' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0552002' AS DateTime2), N'Seed', N'Future Activity 9', N'Activity 9 months in future', N'drinks', CAST(N'2021-04-12T20:29:49.0550374' AS DateTime2), N'Richmond', N'Pub')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'f75728db-f94a-4c7e-be7f-b6fa38354872', CAST(N'2020-07-12T20:29:49.0552038' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0552043' AS DateTime2), N'Seed', N'Future Activity 12', N'Activity 12 months in future', N'music', CAST(N'2021-07-12T20:29:49.0550399' AS DateTime2), N'Seattle', N'Party Place')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'12b96b10-4fba-4b85-8804-b7172f0eaff1', CAST(N'2020-07-12T20:29:49.0551905' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551911' AS DateTime2), N'Seed', N'Future Activity 3', N'Activity 3 months in future', N'drinks', CAST(N'2020-10-12T20:29:49.0550315' AS DateTime2), N'London', N'Another pub')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'8418c88c-f351-4816-b36a-c2bb55cf0825', CAST(N'2020-07-12T20:29:49.0552051' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0552057' AS DateTime2), N'Seed', N'Future Activity 13', N'Activity 13 months in future', N'drinks', CAST(N'2021-08-12T20:29:49.0550407' AS DateTime2), N'Fairfax', N'Disco Place')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'9411f42d-592a-4b01-bee8-db08f9473a48', CAST(N'2020-07-12T20:29:49.0551720' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551805' AS DateTime2), N'Seed', N'Past Activity 1', N'Activity 2 months ago', N'drinks', CAST(N'2020-05-12T20:29:49.0546355' AS DateTime2), N'London', N'Pub')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'e74c5945-1651-42f6-968e-dc460dee53ff', CAST(N'2020-07-15T04:41:48.0445776' AS DateTime2), N'string', CAST(N'2020-07-15T04:41:48.0445697' AS DateTime2), N'string', N'Special 26', N'A group of cons pose as CBI officers and conduct bogus raids to loot politicians and businessmen.', N'film', CAST(N'2021-12-31T06:00:00.0000000' AS DateTime2), N'Himatnagar', N'Star City')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'c10a7210-3887-4c1c-b584-df6f78dee370', CAST(N'2020-07-12T20:29:49.0552024' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0552030' AS DateTime2), N'Seed', N'Future Activity 11', N'Activity 11 months in future', N'music', CAST(N'2021-06-12T20:29:49.0550391' AS DateTime2), N'RVA', N'Bollywood Music')
GO
INSERT [dbo].[Activity] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Title], [Description], [Category], [Date], [City], [Venue]) VALUES (N'5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3', CAST(N'2020-07-12T20:29:49.0551968' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0551974' AS DateTime2), N'Seed', N'Future Activity 7', N'Activity 2 months ago', N'travel', CAST(N'2021-02-12T20:29:49.0550355' AS DateTime2), N'London', N'Somewhere on the Thames')
GO
INSERT [dbo].[AppUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [FirstName], [LastName], [Email], [Bio]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', CAST(N'2020-07-12T20:29:49.0475582' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0475588' AS DateTime2), N'Seed', N'Nij', N'Patel', N'NP@domain.com', NULL)
GO
INSERT [dbo].[AppUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [FirstName], [LastName], [Email], [Bio]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', CAST(N'2020-07-12T20:29:49.0475431' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0475528' AS DateTime2), N'Seed', N'Will', N'Smith', N'Jane.Smith@domain.com', NULL)
GO
INSERT [dbo].[AppUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [FirstName], [LastName], [Email], [Bio]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', CAST(N'2020-07-12T20:29:49.0475567' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0475574' AS DateTime2), N'Seed', N'Jane', N'Murphy', N'Bruce.Lee@domain.com', NULL)
GO
INSERT [dbo].[AppUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [FirstName], [LastName], [Email], [Bio]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', CAST(N'2020-07-12T20:29:49.0440723' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0474200' AS DateTime2), N'Seed', N'John', N'Doe', N'JohnDoe@domain.com', NULL)
GO
INSERT [dbo].[IdentityUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [UserName], [Passoword], [Salt], [RefreshToken], [RefreshTokenExpiry], [AppUserId], [PreviousRefreshToken], [PreviousRefreshTokenExpiry]) VALUES (N'53926a03-90a8-4eeb-b3c4-5c541f9f66b6', CAST(N'2020-07-12T20:29:49.0515763' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0515770' AS DateTime2), N'Seed', N'Bruce.Lee@domain.com', N'5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc=', N'DEX8D+3HR9flD6NpGibucQ==', NULL, NULL, N'08d170bc-a29c-4591-a3e8-abf513d5f936', NULL, NULL)
GO
INSERT [dbo].[IdentityUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [UserName], [Passoword], [Salt], [RefreshToken], [RefreshTokenExpiry], [AppUserId], [PreviousRefreshToken], [PreviousRefreshTokenExpiry]) VALUES (N'fd7a3c5d-c9bd-4a18-8a38-672b2b7f5f96', CAST(N'2020-07-12T20:29:49.0515781' AS DateTime2), N'Seed', CAST(N'2020-07-15T04:25:00.6434471' AS DateTime2), N'', N'string', N'k94BgmW7ByRA20JstnnZy/r4spmr5a43Wj7TOez6Ceg=', N'tycaGrI7zbrlLUa1rlq/Eg==', N'OfsBUXl6pvRR6G693MkcGQ4TabwAZoCEr2YRFUmElCY=', CAST(N'2020-08-14T04:25:00.6424328' AS DateTime2), N'5249a8c8-528e-4090-b404-40d5123e4af4', N'XOrxmhFMslutYcdADhmfg0VV7zkic4Mmt/SgBbfXa1I=', CAST(N'2020-07-15T06:25:00.643' AS DateTime))
GO
INSERT [dbo].[IdentityUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [UserName], [Passoword], [Salt], [RefreshToken], [RefreshTokenExpiry], [AppUserId], [PreviousRefreshToken], [PreviousRefreshTokenExpiry]) VALUES (N'29d18e18-3b07-452d-9caa-7a14f2750d14', CAST(N'2020-07-12T20:29:49.0515254' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0515394' AS DateTime2), N'Seed', N'JohnDoe@domain.com', N'/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0=', N'St0OnTE2Ju3Li9uSnlz/Mg==', NULL, NULL, N'327987e6-fe50-4ebc-9729-fa87348857c1', NULL, NULL)
GO
INSERT [dbo].[IdentityUser] ([Id], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [UserName], [Passoword], [Salt], [RefreshToken], [RefreshTokenExpiry], [AppUserId], [PreviousRefreshToken], [PreviousRefreshTokenExpiry]) VALUES (N'658aaf4c-6c7b-435d-8194-e20b57ff7f0c', CAST(N'2020-07-12T20:29:49.0515740' AS DateTime2), N'Seed', CAST(N'2020-07-12T20:29:49.0515749' AS DateTime2), N'Seed', N'Jane.Smith@domain.com', N'S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o=', N'f9/SzZwluz+xI51/VQQIzg==', NULL, NULL, N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', NULL, NULL)
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'a03b184b-3589-4a5f-9608-179e481be477', 1, N'blob', N'a03b184b-3589-4a5f-9608-179e481be477', N'image/jpeg', 156888, CAST(N'2020-07-15T04:26:13.5187063' AS DateTime2), N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'780105da-9dfb-4df5-91b2-29361ec4abea', 1, N'blob', N'780105da-9dfb-4df5-91b2-29361ec4abea', N'image/jpeg', 38889, CAST(N'2020-07-15T04:05:12.6566512' AS DateTime2), N'cf94be86-baa2-46e2-9efe-a212ca53dc3a')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'8fc3f018-65b8-4eda-b4a9-4105a50463c1', 0, N'blob', N'8fc3f018-65b8-4eda-b4a9-4105a50463c1', N'image/jpeg', 70772, CAST(N'2020-07-15T04:06:16.5955059' AS DateTime2), N'cf94be86-baa2-46e2-9efe-a212ca53dc3a')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'b03446ad-a0b0-4e06-9bf4-8cd813cff253', 0, N'blob', N'b03446ad-a0b0-4e06-9bf4-8cd813cff253', N'image/jpeg', 33844, CAST(N'2020-07-12T20:54:45.3877620' AS DateTime2), N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'f51ac62a-b1a1-41fa-96eb-91bb23cab917', 1, N'blob', N'f51ac62a-b1a1-41fa-96eb-91bb23cab917', N'image/jpeg', 25779, CAST(N'2020-07-15T04:37:39.6302191' AS DateTime2), N'327987e6-fe50-4ebc-9729-fa87348857c1')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'b4d19c50-a100-4e06-9bbf-a68b938422ec', 1, N'blob', N'b4d19c50-a100-4e06-9bbf-a68b938422ec', N'image/jpeg', 15506, CAST(N'2020-07-15T04:30:09.2930700' AS DateTime2), N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'61640992-e955-42cd-9979-a727336a31c1', 0, N'blob', N'61640992-e955-42cd-9979-a727336a31c1', N'image/jpeg', 75793, CAST(N'2020-07-15T04:06:29.7063509' AS DateTime2), N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'e7fafe48-14cd-4c56-9426-aa286fa93b03', 1, N'blob', N'e7fafe48-14cd-4c56-9426-aa286fa93b03', N'image/jpeg', 20233, CAST(N'2020-07-15T04:05:48.2882493' AS DateTime2), N'5249a8c8-528e-4090-b404-40d5123e4af4')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'66c4cf94-68cd-49bb-b33d-ae18e6917c70', 0, N'blob', N'66c4cf94-68cd-49bb-b33d-ae18e6917c70', N'image/jpeg', 187164, CAST(N'2020-07-12T20:30:53.4403327' AS DateTime2), N'5249a8c8-528e-4090-b404-40d5123e4af4')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'098c3f48-cd30-4929-85c6-cc460c0dc79a', 0, N'blob', N'098c3f48-cd30-4929-85c6-cc460c0dc79a', N'image/jpeg', 73392, CAST(N'2020-07-15T04:06:03.3694245' AS DateTime2), N'327987e6-fe50-4ebc-9729-fa87348857c1')
GO
INSERT [dbo].[Photo] ([Id], [IsMainPhoto], [ActualFileName], [CloudFileName], [ContentType], [Length], [UploadedDate], [AppUserId]) VALUES (N'79426065-eb52-4c8e-9e52-fd0f24062bf4', 0, N'blob', N'79426065-eb52-4c8e-9e52-fd0f24062bf4', N'image/jpeg', 32709, CAST(N'2020-07-15T04:06:09.8825777' AS DateTime2), N'327987e6-fe50-4ebc-9729-fa87348857c1')
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'00e6b54a-27bf-4e0e-897f-0773c8c2ada5', CAST(N'2021-01-14T20:29:49.0550346' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'00e6b54a-27bf-4e0e-897f-0773c8c2ada5', CAST(N'2021-01-12T20:29:49.0550346' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'7d9ab25f-2ca9-42fa-b2d9-29d40892a26b', CAST(N'2020-07-12T20:39:16.9966571' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'7d9ab25f-2ca9-42fa-b2d9-29d40892a26b', CAST(N'2020-12-14T20:29:49.0550339' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'7d9ab25f-2ca9-42fa-b2d9-29d40892a26b', CAST(N'2020-12-14T20:29:49.0550339' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'7d9ab25f-2ca9-42fa-b2d9-29d40892a26b', CAST(N'2020-12-12T20:29:49.0550339' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'753d5b55-ff88-4a28-a92c-364c3b68736c', CAST(N'2020-11-14T20:29:49.0550330' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'753d5b55-ff88-4a28-a92c-364c3b68736c', CAST(N'2020-11-12T20:29:49.0550330' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'76a7f1e7-f9ee-4df2-9268-3d7bd4a15578', CAST(N'2021-03-14T20:29:49.0550366' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'76a7f1e7-f9ee-4df2-9268-3d7bd4a15578', CAST(N'2021-03-14T20:29:49.0550366' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'76a7f1e7-f9ee-4df2-9268-3d7bd4a15578', CAST(N'2021-03-12T20:29:49.0550366' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'dd0e2d78-9d7e-41f7-a19e-437430941be3', CAST(N'2020-07-12T20:54:09.1824568' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'd48ac8d3-7ff4-46c8-b652-5d87afbfd1f7', CAST(N'2020-07-15T04:15:16.7532591' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'd48ac8d3-7ff4-46c8-b652-5d87afbfd1f7', CAST(N'2020-08-14T20:29:49.0550297' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'd48ac8d3-7ff4-46c8-b652-5d87afbfd1f7', CAST(N'2020-08-14T20:29:49.0550297' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'd48ac8d3-7ff4-46c8-b652-5d87afbfd1f7', CAST(N'2020-08-12T20:29:49.0550297' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'f74a0ce5-1bfb-4b94-b0c5-663629ec8588', CAST(N'2020-06-12T20:29:49.0550189' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'd3fe056e-2fbd-435b-93f8-79a39142b530', CAST(N'2020-09-14T20:29:49.0550307' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'd3fe056e-2fbd-435b-93f8-79a39142b530', CAST(N'2020-09-12T20:29:49.0550307' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'e102f5ab-0ded-4a3d-96a0-7bafe78ab476', CAST(N'2021-05-12T20:29:49.0550382' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'bcdae06a-2c5e-4019-bdab-b59893df8a08', CAST(N'2021-04-14T20:29:49.0550374' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'bcdae06a-2c5e-4019-bdab-b59893df8a08', CAST(N'2021-04-14T20:29:49.0550374' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'bcdae06a-2c5e-4019-bdab-b59893df8a08', CAST(N'2021-04-12T20:29:49.0550374' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'f75728db-f94a-4c7e-be7f-b6fa38354872', CAST(N'2021-07-12T20:29:49.0550399' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'12b96b10-4fba-4b85-8804-b7172f0eaff1', CAST(N'2020-10-14T20:29:49.0550315' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'12b96b10-4fba-4b85-8804-b7172f0eaff1', CAST(N'2020-10-14T20:29:49.0550315' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'12b96b10-4fba-4b85-8804-b7172f0eaff1', CAST(N'2020-10-12T20:29:49.0550315' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'8418c88c-f351-4816-b36a-c2bb55cf0825', CAST(N'2020-07-12T20:53:28.7430814' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'8418c88c-f351-4816-b36a-c2bb55cf0825', CAST(N'2021-08-14T20:29:49.0550407' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'8418c88c-f351-4816-b36a-c2bb55cf0825', CAST(N'2021-08-14T20:29:49.0550407' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'8418c88c-f351-4816-b36a-c2bb55cf0825', CAST(N'2021-08-12T20:29:49.0550407' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'9411f42d-592a-4b01-bee8-db08f9473a48', CAST(N'2020-05-14T20:29:49.0546355' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'9411f42d-592a-4b01-bee8-db08f9473a48', CAST(N'2020-05-12T20:29:49.0546355' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'e74c5945-1651-42f6-968e-dc460dee53ff', CAST(N'2020-07-15T04:41:48.0420070' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'c10a7210-3887-4c1c-b584-df6f78dee370', CAST(N'2020-07-15T04:20:05.9937308' AS DateTime2), 1)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'c10a7210-3887-4c1c-b584-df6f78dee370', CAST(N'2021-06-12T20:29:49.0550391' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3', CAST(N'2021-02-14T20:29:49.0550355' AS DateTime2), 0)
GO
INSERT [dbo].[UserActivity] ([AppUserId], [ActivityId], [DateJoined], [IsHost]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'5ed64d2e-7dbc-4538-ac0a-eb073eb75bb3', CAST(N'2021-02-12T20:29:49.0550355' AS DateTime2), 1)
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'cf94be86-baa2-46e2-9efe-a212ca53dc3a')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'5249a8c8-528e-4090-b404-40d5123e4af4', N'327987e6-fe50-4ebc-9729-fa87348857c1')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'5249a8c8-528e-4090-b404-40d5123e4af4')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'cf94be86-baa2-46e2-9efe-a212ca53dc3a', N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'cf94be86-baa2-46e2-9efe-a212ca53dc3a')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'08d170bc-a29c-4591-a3e8-abf513d5f936', N'327987e6-fe50-4ebc-9729-fa87348857c1')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'5249a8c8-528e-4090-b404-40d5123e4af4')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'cf94be86-baa2-46e2-9efe-a212ca53dc3a')
GO
INSERT [dbo].[UserFollower] ([UserId], [FollowerId]) VALUES (N'327987e6-fe50-4ebc-9729-fa87348857c1', N'08d170bc-a29c-4591-a3e8-abf513d5f936')
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Activity_ActivityId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_AppUser_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[AppUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_AppUser_AuthorId]
GO
ALTER TABLE [dbo].[IdentityUser]  WITH CHECK ADD  CONSTRAINT [FK_IdentityUser_AppUser_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IdentityUser] CHECK CONSTRAINT [FK_IdentityUser_AppUser_AppUserId]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_AppUser_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_AppUser_AppUserId]
GO
ALTER TABLE [dbo].[UserActivity]  WITH CHECK ADD  CONSTRAINT [FK_UserActivity_Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserActivity] CHECK CONSTRAINT [FK_UserActivity_Activity_ActivityId]
GO
ALTER TABLE [dbo].[UserActivity]  WITH CHECK ADD  CONSTRAINT [FK_UserActivity_AppUser_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUser] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserActivity] CHECK CONSTRAINT [FK_UserActivity_AppUser_AppUserId]
GO
ALTER TABLE [dbo].[UserFollower]  WITH CHECK ADD  CONSTRAINT [FK_UserFollower_AppUser_FollowerId] FOREIGN KEY([FollowerId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[UserFollower] CHECK CONSTRAINT [FK_UserFollower_AppUser_FollowerId]
GO
ALTER TABLE [dbo].[UserFollower]  WITH CHECK ADD  CONSTRAINT [FK_UserFollower_AppUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[UserFollower] CHECK CONSTRAINT [FK_UserFollower_AppUser_UserId]
GO
