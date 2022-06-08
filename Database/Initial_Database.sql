USE [master]
GO
/****** Object:  Database [learning_apsnet_mvc]    Script Date: 2/4/2022 10:57:30 PM ******/
CREATE DATABASE [learning_apsnet_mvc]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'learning_apsnet_mvc', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\learning_apsnet_mvc.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'learning_apsnet_mvc_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\learning_apsnet_mvc_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET COMPATIBILITY_LEVEL = 130
--GO
--IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
--begin
--EXEC [learning_apsnet_mvc].[dbo].[sp_fulltext_database] @action = 'enable'
--end
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ANSI_NULL_DEFAULT OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ANSI_NULLS OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ANSI_PADDING OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ANSI_WARNINGS OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ARITHABORT OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET AUTO_CLOSE OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET AUTO_SHRINK OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET AUTO_UPDATE_STATISTICS ON 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET CURSOR_CLOSE_ON_COMMIT OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET CURSOR_DEFAULT  GLOBAL 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET CONCAT_NULL_YIELDS_NULL OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET NUMERIC_ROUNDABORT OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET QUOTED_IDENTIFIER OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET RECURSIVE_TRIGGERS OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET  DISABLE_BROKER 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET DATE_CORRELATION_OPTIMIZATION OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET TRUSTWORTHY OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET PARAMETERIZATION SIMPLE 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET READ_COMMITTED_SNAPSHOT OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET HONOR_BROKER_PRIORITY OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET RECOVERY SIMPLE 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET  MULTI_USER 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET PAGE_VERIFY CHECKSUM  
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET DB_CHAINING OFF 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET TARGET_RECOVERY_TIME = 0 SECONDS 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET DELAYED_DURABILITY = DISABLED 
--GO
--ALTER DATABASE [learning_apsnet_mvc] SET QUERY_STORE = OFF
--GO
USE [learning_apsnet_mvc]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [learning_apsnet_mvc]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[TotalPrice] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Details]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [nvarchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[TotalPrice] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Order_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[MiddleName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 2/4/2022 10:57:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [learning_apsnet_mvc] SET  READ_WRITE 
GO
