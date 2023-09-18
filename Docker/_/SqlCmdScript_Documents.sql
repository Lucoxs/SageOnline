USE [master]
GO

/****** Object:  Database [IdentityConfig]    Script Date: 16/09/2023 19:11:14 ******/
CREATE DATABASE [IdentityConfig]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IdentityConfig', FILENAME = N'/var/opt/mssql/data/IdentityConfig.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IdentityConfig_log', FILENAME = N'/var/opt/mssql/data/IdentityConfig_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IdentityConfig].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IdentityConfig] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [IdentityConfig] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [IdentityConfig] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [IdentityConfig] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [IdentityConfig] SET ARITHABORT OFF 
GO

ALTER DATABASE [IdentityConfig] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [IdentityConfig] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [IdentityConfig] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [IdentityConfig] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [IdentityConfig] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [IdentityConfig] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [IdentityConfig] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [IdentityConfig] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [IdentityConfig] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [IdentityConfig] SET  DISABLE_BROKER 
GO

ALTER DATABASE [IdentityConfig] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [IdentityConfig] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [IdentityConfig] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [IdentityConfig] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [IdentityConfig] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [IdentityConfig] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [IdentityConfig] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [IdentityConfig] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [IdentityConfig] SET  MULTI_USER 
GO

ALTER DATABASE [IdentityConfig] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [IdentityConfig] SET DB_CHAINING OFF 
GO

ALTER DATABASE [IdentityConfig] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [IdentityConfig] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [IdentityConfig] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [IdentityConfig] SET QUERY_STORE = OFF
GO

USE [IdentityConfig]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [IdentityConfig] SET  READ_WRITE 
GO


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[co_company]    Script Date: 16/09/2023 19:13:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 16/09/2023 19:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 16/09/2023 19:14:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 16/09/2023 19:15:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 16/09/2023 19:15:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 16/09/2023 19:16:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 16/09/2023 19:16:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


USE [IdentityConfig]
GO

/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 16/09/2023 19:16:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


