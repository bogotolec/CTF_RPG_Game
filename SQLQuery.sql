USE [RPG_CTF_DB]
GO

CREATE TABLE [dbo].[GameUsers](
	[CharacterId] int IDENTITY(1, 1),
	[UserLogin] [varchar](50) NOT NULL,
	[PasswordHash] [char](32) NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GameUsers]
ADD CONSTRAINT [DB_GameUsers_UserLogin_Unique] UNIQUE (UserLogin)
GO

ALTER TABLE [dbo].[GameUsers]
ADD CONSTRAINT [DB_GameUsers_CharacterId] PRIMARY KEY CLUSTERED (CharacterId)
GO

CREATE TABLE [dbo].[GameCharacters](
	[Id] int NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Lvl] int NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[SkillPoints] int NOT NULL,
	[HeadId] int NOT NULL,
	[BodyId] int NOT NULL,
	[LHandId] int NOT NULL,
	[RHandId] int NOT NULL,
	[Boots] int NOT NULL,
	[JeweleryOne] int NOT NULL,
	[JeweleryTwo] int NOT NULL,
	[Gold] int NOT NULL,
	[Health] int NOT NULL,
	[BackPack] [varchar](500) NOT NULL,
	[LearnedSkills] [varchar](500) NOT NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[GameCharacters]
ADD CONSTRAINT [DB_GameCharacters_Id] PRIMARY KEY CLUSTERED (Id)
GO