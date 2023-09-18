USE [master]
GO

CREATE DATABASE [IdentityConfig];
GO

USE [IdentityConfig]
GO

CREATE TABLE [dbo].[co_company](
	[co_id] [int] IDENTITY(1,1) NOT NULL,
	[co_name] [nvarchar](255) NOT NULL,
	[co_activity] [nvarchar](255) NULL,
	[co_legal_status] [nvarchar](255) NULL,
	[co_capital] [float] NULL,
	[co_address] [nvarchar](255) NULL,
	[co_complement] [nvarchar](255) NULL,
	[co_zip] [nvarchar](255) NULL,
	[co_city] [nvarchar](255) NULL,
	[co_region] [nvarchar](255) NULL,
	[co_country] [nvarchar](255) NULL,
	[co_siret] [nvarchar](255) NULL,
	[co_vat_identifier] [nvarchar](255) NULL,
	[co_naf_code] [nvarchar](255) NULL,
	[co_website] [nvarchar](255) NULL,
	[co_phone] [nvarchar](255) NULL,
	[co_email] [nvarchar](255) NULL,
	[co_max_users] [int] NOT NULL,
 CONSTRAINT [PK_co_company] PRIMARY KEY CLUSTERED 
(
	[co_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[us_firstname] [nvarchar](255) NOT NULL,
	[us_lastname] [nvarchar](255) NOT NULL,
	[co_id] [int] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_co_company_co_id] FOREIGN KEY([co_id])
REFERENCES [dbo].[co_company] ([co_id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_co_company_co_id]
GO


CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO


CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO




CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO



CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO




CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO


CREATE TABLE [dbo].[DataProtectionKeys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FriendlyName] [nvarchar](max) NULL,
	[Xml] [nvarchar](max) NULL,
 CONSTRAINT [PK_DataProtectionKeys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO







INSERT INTO [dbo].[co_company]
           ([co_name]
           ,[co_activity]
           ,[co_legal_status]
           ,[co_capital]
           ,[co_address]
           ,[co_complement]
           ,[co_zip]
           ,[co_city]
           ,[co_region]
           ,[co_country]
           ,[co_siret]
           ,[co_vat_identifier]
           ,[co_naf_code]
           ,[co_website]
           ,[co_phone]
           ,[co_email]
           ,[co_max_users])
     VALUES
           ('Sage',
		   'Éditeur de logiciel',
		   'SAS',
		   1000000,
		   '10 place de Belgique',
		   null,
		   '92250',
		   'La Garenne Colombes',
		   'Île-de-France',
		   'France',
		   '55555',
		   '999999999',
		   '010203',
		   'https://www.sage.com/',
		   '0810 30 30 30',
		   'contact@sage.com',
		   5)
GO

INSERT INTO [dbo].[co_company]
           ([co_name]
           ,[co_activity]
           ,[co_legal_status]
           ,[co_capital]
           ,[co_address]
           ,[co_complement]
           ,[co_zip]
           ,[co_city]
           ,[co_region]
           ,[co_country]
           ,[co_siret]
           ,[co_vat_identifier]
           ,[co_naf_code]
           ,[co_website]
           ,[co_phone]
           ,[co_email]
           ,[co_max_users])
     VALUES
           ('BLC-Conseil',
		   'Revendeur de logiciel',
		   'SASU',
		   18000,
		   '94 rue Saint-Lazare',
		   'Batiment D',
		   '75009',
		   'Paris',
		   'Île-de-France',
		   'France',
		   '55555',
		   '999999999',
		   '010203',
		   'https://www.blc-conseil.com/',
		   '0102030405',
		   'contact@blc-conseil.com',
		   12)
		   
GO



INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
VALUES ('28a74bf7-2ddf-4870-a1fe-bd077286b92d', 'admin', 'ADMIN')
GO

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
VALUES ('2bab3f59-0108-49aa-ac01-5b533f903531', 'super_admin', 'SUPER_ADMIN')
GO

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
VALUES ('7cce1b84-d1e5-4e02-bb57-219ad221613a', 'user', 'USER')
GO

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
VALUES ('d06e9fab-0807-4ec2-99ee-c760ab70e98c', 'openid', 'OPENID')
GO

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
VALUES ('e1d88ffa-dfdd-4600-9681-2656f0fdc725', 'offline_access', 'OFFLINE_ACCESS')
GO



INSERT INTO [dbo].[AspNetUsers] ([Id],[us_firstname],[us_lastname],[co_id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount)
VALUES ('CD77AC88-B213-497A-8348-2701056B85BE', 'Lucas', 'Strohl', 2, 'Strohl_Lucas', 'STROHL_LUCAS', 'lucas@blc-conseil.com', 'LUCAS@BLC-CONSEIL.COM', 0, 0, 0, 0, 0)
GO

INSERT INTO [dbo].[AspNetUsers] ([Id],[us_firstname],[us_lastname],[co_id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount)
VALUES ('8702F32D-28AA-4744-894E-6801F70C23B8', 'Olivier', 'Coujandassamy', 1, 'Coujandassamy_Olivier', 'COUJANDASSAMY_OLIVIER', 'olivier@sage.com', 'OLIVIER@SAGE.COM', 0, 0, 0, 0, 0)
GO

INSERT INTO [dbo].[AspNetUsers] ([Id],[us_firstname],[us_lastname],[co_id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],EmailConfirmed,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount)
VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336', 'Sébastien', 'Texier', 1, 'Texier_Sébastien', 'TEXIER_SEBASTIEN', 'Sébastien@sage.com', 'SEBASTIEN@SAGE.COM', 0, 0, 0, 0, 0)
GO




INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336' ,'28a74bf7-2ddf-4870-a1fe-bd077286b92d')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336' ,'2bab3f59-0108-49aa-ac01-5b533f903531')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336' ,'7cce1b84-d1e5-4e02-bb57-219ad221613a')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336' ,'d06e9fab-0807-4ec2-99ee-c760ab70e98c')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('35A35029-3F92-4BCB-9F15-0433C83A1336' ,'e1d88ffa-dfdd-4600-9681-2656f0fdc725')
GO

INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('8702F32D-28AA-4744-894E-6801F70C23B8' ,'7cce1b84-d1e5-4e02-bb57-219ad221613a')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('8702F32D-28AA-4744-894E-6801F70C23B8' ,'d06e9fab-0807-4ec2-99ee-c760ab70e98c')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('8702F32D-28AA-4744-894E-6801F70C23B8' ,'e1d88ffa-dfdd-4600-9681-2656f0fdc725')
GO

INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('CD77AC88-B213-497A-8348-2701056B85BE' ,'28a74bf7-2ddf-4870-a1fe-bd077286b92d')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('CD77AC88-B213-497A-8348-2701056B85BE' ,'7cce1b84-d1e5-4e02-bb57-219ad221613a')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('CD77AC88-B213-497A-8348-2701056B85BE' ,'d06e9fab-0807-4ec2-99ee-c760ab70e98c')
GO
INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId])
     VALUES ('CD77AC88-B213-497A-8348-2701056B85BE' ,'e1d88ffa-dfdd-4600-9681-2656f0fdc725')
GO